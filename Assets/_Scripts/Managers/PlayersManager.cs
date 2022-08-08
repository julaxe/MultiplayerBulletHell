using System;
using System.Collections.Generic;
using _Scripts.Utilities;


namespace _Scripts.Managers
{
    public class PlayersManager : Singleton<PlayersManager>
    {
        public static event Action<bool> PlayerIsReadyChanged;
        public List<Player> ConnectedPlayers;
        public bool isPlayer1;
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
            if (ConnectedPlayers.Count <= 1) return null;
            return ConnectedPlayers[1];
        }
    }
}