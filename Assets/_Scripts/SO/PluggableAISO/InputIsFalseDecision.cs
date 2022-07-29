using _Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.SO.PluggableAISO
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/InputIsFalse")]
    public class InputIsFalseDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            return IsInputFalse(controller);
        }

        private bool IsInputFalse(StateController controller)
        {
            return !Touchscreen.current.primaryTouch.isInProgress;
        }
    }
}