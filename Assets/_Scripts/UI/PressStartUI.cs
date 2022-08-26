using System;
using _Scripts.Managers;
using FishNet;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.UI
{
    public class PressStartUI : MonoBehaviour
    {
        private void Update()
        {
            if (Touchscreen.current.primaryTouch.isInProgress)
            {
                GameManager.Instance.ChangeState(GameState.MainMenu);
            }
        }
    }
}