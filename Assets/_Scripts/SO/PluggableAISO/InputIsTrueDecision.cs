using _Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.SO.PluggableAISO
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/InputIsTrue")]
    public class InputIsTrueDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return IsInputTrue(controller);
        }

        private bool IsInputTrue(StateController controller)
        {
            if (Touchscreen.current.touches.Count > 0)
            {
                Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(
                    Touchscreen.current.primaryTouch.position.ReadValue());
                var isTouchingEnemy = Helpers.IsPointInsideCircle(positionOnScreen, controller.transform.position, 
                    controller.radius);
                if (isTouchingEnemy)
                    return true;
            }
            return false;
        }
    }
}