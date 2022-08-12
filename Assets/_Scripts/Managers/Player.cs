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
        
        [SyncVar] public float hp = 100.0f;
        [SyncVar] public int score = 0;

        [SyncVar] public bool isPlayer1 = false;

        public PlayerInputManager playerInput;

        public PlayerInput input;

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

        private void Start()
        {
            if(!isPlayer1) RotateCamera();
        }

        public void TakeDamage(float damage)
        {
            if (hp == 0.0f) return;

            hp -= damage;
            if (hp < 0.0f) hp = 0.0f;
        }
        
        private void RotateCamera()
        {
            _mainCamera = Camera.main;
            _mainCamera.transform.Rotate(new Vector3(0.0f,0.0f,180.0f));
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