using System;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;


namespace _Scripts.Managers
{
    public class PlayersManager : NetworkBehaviour
    {
        public static PlayersManager Instance { get; private set; }
        public static event Action<bool> PlayerIsReadyChanged;
        [SyncVar] public Player player1;
        [SyncVar] public Player player2;

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            Instance = this;
        }

        [ServerRpc(RequireOwnership = false)]
        public void AddPlayer(Player player)
        {
            if (player1 == null)
            {
                player1 = player;
                player1.isPlayer1 = true;
            }
            else player2 = player;
        }
        
        [TargetRpc]
        public void ChangeStateToSpecificClient(NetworkConnection conn, GameState newState)
        {
            GameManager.Instance.ChangeState(newState);
        }
        
        public void InvokePlayerReadyChanged(bool value)
        {
            PlayerIsReadyChanged?.Invoke(value);
        }
        
        public bool PlayersAreReady()
        {
            if (player1 != null && !player1.isReady) return false;
            if (player2 != null && !player2.isReady) return false;
            return true;
        }

        public Player GetPlayer1()
        {
            return player1 != null ? player1 : null;
        }
        public Player GetPlayer2()
        {
            return player2 != null ? player2 : null;
        }

        public bool Player2Exists()
        {
            return player2 != null;
        }

        public void SpawnPlayersPrefabs()
        {
            if(player1 != null)
                player1.ServerSpawnPlayerPrefab(player1.Owner);
            if(player2 != null)
                player2.ServerSpawnPlayerPrefab(player2.Owner);
        }
    }
}