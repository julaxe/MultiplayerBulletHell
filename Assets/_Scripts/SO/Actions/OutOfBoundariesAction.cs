using _Scripts.Units.Enemies;
using UnityEngine;

namespace SO.Actions
{
    [CreateAssetMenu(fileName = "OutOfBoundariesAction", menuName = "PluggableAI/Actions/OutOfBoundaries", order = 0)]
    public class OutOfBoundariesAction : Action
    {
        public float YBoundary = 20f;
        public override void Act(StateController controller)
        {
            OutOfBoundaries(controller);
        }

        private void OutOfBoundaries(StateController controller)
        {
            if (Mathf.Abs(controller.transform.position.y) > YBoundary)
            {
                controller.gameObject.SetActive(false);
            }
        }
    }
}