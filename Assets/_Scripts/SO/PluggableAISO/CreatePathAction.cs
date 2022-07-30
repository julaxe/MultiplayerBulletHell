using _Scripts.Units.Enemies;
using UnityEngine;

namespace _Scripts.SO.PluggableAISO
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
            Destroy(controller.arrowLine.gameObject);
        }
    }
}