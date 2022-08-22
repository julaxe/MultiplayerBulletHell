using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Managers
{
    public class PlayerInputManager : MonoBehaviour
    {
        public bool isMoving;

        public void OnLeftClick(InputValue value)
        {
            if (GameManager.Instance.PlayState == PlayState.Spawning)
            {
                isMoving = false;
                return;
            }
            isMoving = value.isPressed;
        }
    }
}