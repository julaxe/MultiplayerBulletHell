using System;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Managers
{
    public class Player : NetworkBehaviour
    {
        public static Player Instance { get; private set; }

        public GameObject playerPrefab;
        
        private Camera _mainCamera;
        
        [SyncVar(OnChange = nameof(OnTakeDamage))] public float hp = 100.0f;
        [SyncVar(OnChange = nameof(OnScore))] public int score = 0;

        [SyncVar] public bool isPlayer1 = false;

        public PlayerInputManager playerInput;

        public PlayerInput input;

        [SerializeField] private AudioClip damageClip;

        //Used in the lobby
        [SyncVar(OnChange = nameof(IsReadyIsChanged))] public bool isReady;

        public override void OnStartClient()
        {
            base.OnStartClient();
            
            playerInput.enabled =  IsOwner;
            input.enabled = IsOwner;
            
            if (!IsOwner) return;
            PlayersManager.Instance.AddPlayer(this);
            
            
            Instance = this;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            hp = 100;
            score = 0;
        }

        public void TakeDamage(float damage)
        {
            if (hp == 0.0f) return;
            
            hp -= damage;
            if (hp < 0.0f) hp = 0.0f;
        }

        private void OnTakeDamage(float previous, float current, bool onServer)
        {
            if (!IsOwner) return;
            AudioSystem.Instance.PlaySound(damageClip);
            UIManager.Instance.AnimatePlayerHealth();
        }

        private void OnScore(int previous, int current, bool onServer)
        {
            if (!IsOwner) return;
            UIManager.Instance.AnimatePlayerScore();
        }
        
        
        #region IsReady
        private void IsReadyIsChanged(bool prev, bool actual, bool asServer)
        {
            if(IsOwner)
                PlayersManager.Instance.InvokePlayerReadyChanged(actual);
        }
        
        [ServerRpc]
        public void ServerSetIsReady(bool value)
        {
            isReady = value;
        }
        

        #endregion
       

        #region ChangeState
        [ServerRpc]
        public void ChangePlayStateForEveryBody(PlayState newState)
        {
            //server-side
            GameManager.Instance.ChangePlayState(newState);
            
            //clients
            ChangePlayStateOnAllClients(newState);
        }

        [ObserversRpc]
        public void ChangePlayStateOnAllClients(PlayState newState)
        {
            GameManager.Instance.ChangePlayState(newState);
        }

        [ServerRpc]
        public void SwitchBetweenShootingAndSpawning(PlayState currentState)
        {
            switch (currentState)
            {
                case PlayState.Spawning:
                    PlayersManager.Instance.ChangeStateToSpecificClient(Owner, PlayState.Shooting);
                    if(PlayersManager.Instance.Player2Exists())
                        PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, PlayState.Spawning);
                    break;
                case PlayState.Shooting:
                    PlayersManager.Instance.ChangeStateToSpecificClient(Owner, PlayState.Spawning);
                    if(PlayersManager.Instance.Player2Exists())
                        PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, PlayState.Shooting);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentState), currentState, null);
            }
        }

        public void SwitchBetweenShootingAndSpawning()
        {
            switch (GameManager.Instance.PlayState)
            {
                case PlayState.Shooting:
                    GameManager.Instance.ChangePlayState(PlayState.Spawning);
                    break;
                case PlayState.Spawning:
                    GameManager.Instance.ChangePlayState(PlayState.Shooting);
                    break;
                default:
                    break;
            }
        }




        #endregion


        public void ServerSpawnPlayerPrefab(NetworkConnection ownerConnection)
        {
            var newPrefab = Instantiate(playerPrefab);
            InstanceFinder.ServerManager.Spawn(newPrefab, ownerConnection);
        }

        [ServerRpc]
        public void ServerSpawnPrefab(GameObject prefab)
        {
            var temp = Instantiate(prefab);
            Spawn(temp, Owner);
        }
        
    }
}