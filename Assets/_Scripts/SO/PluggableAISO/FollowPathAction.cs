using _Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.SO.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/FollowPath")]
    public class FollowPathAction : Action
    {
        public float resolution = 0.05f;
        public override void OnEnter(StateController controller)
        {
            controller.currentArrowLinePosition = 0;
        }

        public override void Act(StateController controller)
        {
            FollowPath(controller);
        }

        private void FollowPath(StateController controller)
        {
            if (controller.currentArrowLinePosition >= controller.arrowLinePositions.Length) return;
            
            GoToNextPoint(controller);
            var distance = Vector3.Distance(controller.transform.position,
                controller.arrowLinePositions[controller.currentArrowLinePosition]);
            if (distance <= resolution)
            {
                controller.currentArrowLinePosition += 1;
            }
            
        }

        private void GoToNextPoint(StateController controller)
        {
            var newPos = Vector3.Lerp(controller.transform.position,
                controller.arrowLinePositions[controller.currentArrowLinePosition], resolution);
            controller.transform.position = newPos;
        }
    }
}