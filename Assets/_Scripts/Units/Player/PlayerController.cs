using System;
using _Scripts.Managers;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

namespace _Scripts.Units.Player
{
    public class PlayerController : NetworkBehaviour
    {
        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            enabled = IsServer;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;
            
            if (Owner == PlayersManager.Instance.GetPlayer1().Owner)
            {
                PlayersManager.Instance.GetPlayer1().TakeDamage(10);
                
            }
            else
            {
                PlayersManager.Instance.GetPlayer2().TakeDamage(10);
            }
        }
        
    }
}