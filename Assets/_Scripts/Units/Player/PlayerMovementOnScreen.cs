using _Scripts.Managers;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Player
{
    public class PlayerMovementOnScreen : NetworkBehaviour
    {

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            enabled = IsOwner;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.State != GameState.Shooting) return;
            if (!Managers.Player.Instance.playerInput.isMoving) return;
            Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
            transform.position = positionOnScreen;
        }
        
    }
}