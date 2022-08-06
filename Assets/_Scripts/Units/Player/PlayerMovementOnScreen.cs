using _Scripts.Managers;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Player
{
    public class PlayerMovementOnScreen : NetworkBehaviour
    {
        private bool _isMoving;
        private PlayerInput _playerInput;
        [SerializeField] private bool _isOwner;

        public void OnNetworkSpawn()
        {
            _isOwner = IsOwner;
            _playerInput = GetComponent<PlayerInput>();
            if (IsOwner) return;
            _playerInput.enabled = false;
            enabled = false;
        }
        

        private void FixedUpdate()
        {
            if (GameManager.Instance.State != GameState.Shooting) return;
            if (!_isMoving) return;
            Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
            transform.position = positionOnScreen;
        }

        public void OnLeftClick(InputValue value)
        {
            if (GameManager.Instance.State != GameState.Shooting) return;
            _isMoving = value.isPressed;
        }
    }
}