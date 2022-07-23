using System;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionServer : NetworkBehaviour
    {
        [SerializeField] private NetworkSO networkSo;

        private static int playerNumberCounter = 0;
        private int playerNumberId;
        public override void OnNetworkSpawn()
        {
            networkSo.player1Info.ResetValues();
            networkSo.player2Info.ResetValues();
            playerNumberCounter += 1;
            playerNumberId = playerNumberCounter;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;
            Debug.Log("Collision with player " + playerNumberId + " with " + other.name);
            DoDamage_ClientRpc(10.0f, playerNumberId == 1);
        }
        
        [ClientRpc]
        private void DoDamage_ClientRpc(float damage, bool isPlayer1)
        {
            if (isPlayer1)
            {
                networkSo.player1Info.TakeDamage(damage);
            }
            else
            {
                networkSo.player2Info.TakeDamage(damage);
            }
        }
    }
}