using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovementOnScreen : MonoBehaviour
    {
        private bool _isMoving;
        private void FixedUpdate()
        {
            if (GameManager.Instance.State != GameState.Shooting) return;
            if (!_isMoving) return;
            Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
            transform.position = positionOnScreen;
        }

        public void OnLeftClick(InputValue value)
        {
            _isMoving = value.isPressed;
        }
    }
}