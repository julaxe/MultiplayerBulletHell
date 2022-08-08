using System;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace _Scripts.Managers
{
    public class Player : NetworkBehaviour
    {
        public static Player Instance { get; private set; }

        //Used in the lobby
        [SyncVar(OnChange = nameof(IsReadyIsChanged))] public bool isReady;

        public override void OnStartClient()
        {
            base.OnStartClient();
            PlayersManager.Instance.ConnectedPlayers.Add(this);
            
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
                    ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Spawning);
                    break;
                case GameState.Shooting:
                    ChangeStateToSpecificClient(Owner, GameState.Spawning);
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
        
    }
}