using _Scripts.Managers;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Player
{
    public class PlayerMovementOnScreen : NetworkBehaviour
    {

        private Camera _mainCamera;

        private Camera MainCamera
        {
            get
            {
                if (_mainCamera != null) return _mainCamera;
                else
                {
                    _mainCamera = FindObjectOfType<Camera>();
                    return _mainCamera;
                }
            }
        }
        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            enabled = IsOwner;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.State != GameState.Shooting) return;
            if (!Managers.Player.Instance.playerInput.isMoving) return;
            Vector2 positionOnScreen = MainCamera.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
            transform.position = positionOnScreen;
        }
        
    }
}