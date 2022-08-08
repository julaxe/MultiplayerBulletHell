using System;
using _Scripts.Managers;
using FishNet.Object;
using SO;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionServer : NetworkBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;
            //Debug.Log("Collision with player " + playerNumberId + " with " + other.name);
            //DoDamage_ClientRpc(10.0f, playerNumberId == 1);
        }
        
    }
}