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

        public bool isPlayer1;

        public PlayerInputManager playerInput;

        public PlayerInput input;

        //Used in the lobby
        [SyncVar(OnChange = nameof(IsReadyIsChanged))] public bool isReady;

        public override void OnStartClient()
        {
            base.OnStartClient();
            PlayersManager.Instance.ConnectedPlayers.Add(this);
            isPlayer1 = PlayersManager.Instance.ConnectedPlayers.Count == 1;
            playerInput.enabled =  IsOwner;
            input.enabled = IsOwner;
            
            if (!IsOwner) return;
            Instance = this;
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
                    ChangeStateToSpecificClient(Owner, GameState.Shooting);
                    if(PlayersManager.Instance.Player2Exists())
                        ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Spawning);
                    break;
                case GameState.Shooting:
                    ChangeStateToSpecificClient(Owner, GameState.Spawning);
                    if(PlayersManager.Instance.Player2Exists())
                        ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Shooting);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentState), currentState, null);
            }
        }

        [TargetRpc]
        public void ChangeStateToSpecificClient(NetworkConnection conn, GameState newState)
        {
            GameManager.Instance.ChangeState(newState);
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