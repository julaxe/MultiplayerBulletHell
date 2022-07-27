using System;
using _Scripts.Managers;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.UI
{
    public class LobbyUI : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            gameObject.SetActive(IsHost);
        }

        public void StartPressed()
        {
            Start_ServerRpc();
        }

        [ServerRpc]
        private void Start_ServerRpc()
        {
            Start_ClientRpc();
        }

        [ClientRpc]
        private void Start_ClientRpc()
        {
            GameManager.Instance.ChangeState(PlayersManager.Instance.isPlayer1? GameState.Shooting : GameState.Spawning);
        }
    }
}