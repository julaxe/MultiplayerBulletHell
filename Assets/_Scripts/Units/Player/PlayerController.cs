using System;
using _Scripts.Managers;
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
                Debug.Log("Collision in Player 1");
                PlayersManager.Instance.GetPlayer1().TakeDamage(10);
            }
            else
            {
                Debug.Log("Collision in Player 2");
                PlayersManager.Instance.GetPlayer2().TakeDamage(10);
            }
        }
    }
}