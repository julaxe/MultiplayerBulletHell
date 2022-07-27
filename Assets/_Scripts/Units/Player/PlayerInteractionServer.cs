using System;
using _Scripts.Managers;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionServer : NetworkBehaviour
    {
        private static int playerNumberCounter = 0;
        private int playerNumberId;
        public override void OnNetworkSpawn()
        {
            PlayersManager.Instance.player1Info.ResetValues();
            PlayersManager.Instance.player2Info.ResetValues();
            playerNumberCounter += 1;
            playerNumberId = playerNumberCounter;
            //GameManager.Instance.ChangeState(PlayersManager.Instance.isPlayer1 ? GameState.Shooting : GameState.Spawning);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;
            //Debug.Log("Collision with player " + playerNumberId + " with " + other.name);
            DoDamage_ClientRpc(10.0f, playerNumberId == 1);
        }
        
        [ClientRpc]
        private void DoDamage_ClientRpc(float damage, bool isPlayer1)
        {
            if (isPlayer1)
            {
                PlayersManager.Instance.player1Info.TakeDamage(damage);
            }
            else
            {
                PlayersManager.Instance.player2Info.TakeDamage(damage);
            }
        }
    }
}