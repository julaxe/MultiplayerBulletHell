using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Managers
{
    public class PlayerInputManager : MonoBehaviour
    {
        public bool isMoving;

        public void OnLeftClick(InputValue value)
        {
            if (GameManager.Instance.State == GameState.Spawning)
            {
                isMoving = false;
                return;
            }
            isMoving = value.isPressed;
        }
    }
}