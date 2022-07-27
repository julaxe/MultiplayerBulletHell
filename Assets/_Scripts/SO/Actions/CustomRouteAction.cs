using Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SO.Actions
{
    [CreateAssetMenu(fileName = "CustomRouteAction", menuName = "PluggableAI/Actions/CustomRouteAction")]
    public class CustomRouteAction : Action
    {
        public float speed = 1.0f;
        public float timeRecorder;
        public override void Act(StateController controller)
        {
            CheckForTouchScreen();
        }

        private void CheckForTouchScreen()
        {
            bool touchPosition = Touchscreen.current.primaryTouch.press.isPressed;
        }
    }
}