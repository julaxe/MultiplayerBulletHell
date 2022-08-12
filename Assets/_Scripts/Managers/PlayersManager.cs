using System;
using System.Collections.Generic;

using FishNet.Object;
using FishNet.Object.Synchronizing;


namespace _Scripts.Managers
{
    public class PlayersManager : NetworkBehaviour
    {
        public static PlayersManager Instance { get; private set; }
        public static event Action<bool> PlayerIsReadyChanged;
        [SyncVar] public List<Player> ConnectedPlayers;

        public override void OnStartServer()
        {
            base.OnStartServer();
            Instance = this;
        }

        public void InvokePlayerReadyChanged(bool value)
        {
            PlayerIsReadyChanged?.Invoke(value);
        }

        public bool PlayersAreReady()
        {
            foreach (var player in ConnectedPlayers)
            {
                if (!player.isReady) return false;
            }
            return true;
        }

        public Player GetPlayer1()
        {
            return ConnectedPlayers[0];
        }
        public Player GetPlayer2()
        {
            if (!Player2Exists()) return null;
            return ConnectedPlayers[1];
        }

        public bool Player2Exists()
        {
            return ConnectedPlayers.Count > 1;
        }

        public void SpawnPlayersPrefabs()
        {
            foreach (var player in ConnectedPlayers)
            {
                player.ServerSpawnPlayerPrefab(player.Owner);
            }
        }
    }
}