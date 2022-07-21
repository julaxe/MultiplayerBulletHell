using System;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionServer : NetworkBehaviour
    {
        [SerializeField] private NetworkSO networkSo;

        public override void OnNetworkSpawn()
        {
            networkSo.player1Info.ResetValues();
            networkSo.player2Info.ResetValues();
        }
    }
}