using _Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.SO.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/CreatePath")]
    public class CreatePathAction : Action
    {
        public override void Act(StateController controller)
        {
            
        }

        public override void OnExit(StateController controller)
        {
            controller.arrowLinePositions = controller.arrowLine.GetPositions();
        }
    }
}