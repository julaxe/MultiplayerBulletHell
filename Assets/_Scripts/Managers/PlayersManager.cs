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
    }
}