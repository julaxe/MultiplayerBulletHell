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
        public void ChangeStateForEveryBody(GameState newState)
        {
            //server-side
            GameManager.Instance.ChangeState(newState);
            
            //clients
            ChangeStateOnAllClients(newState);
        }

        [ObserversRpc]
        public void ChangeStateOnAllClients(GameState newState)
        {
            GameManager.Instance.ChangeState(newState);
        }

        [ServerRpc]
        public void SwitchBetweenShootingAndSpawning(GameState currentState)
        {
            switch (currentState)
            {
                case GameState.Spawning:
                    PlayersManager.Instance.ChangeStateToSpecificClient(Owner, GameState.Shooting);
                    if(PlayersManager.Instance.Player2Exists())
                        PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Spawning);
                    break;
                case GameState.Shooting:
                    PlayersManager.Instance.ChangeStateToSpecificClient(Owner, GameState.Spawning);
                    if(PlayersManager.Instance.Player2Exists())
                        PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Shooting);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentState), currentState, null);
            }
        }

        public void SwitchBetweenShootingAndSpawning()
        {
            switch (GameManager.Instance.State)
            {
                case GameState.Shooting:
                    GameManager.Instance.ChangeState(GameState.Spawning);
                    break;
                case GameState.Spawning:
                    GameManager.Instance.ChangeState(GameState.Shooting);
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