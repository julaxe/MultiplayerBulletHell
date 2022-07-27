using Enemies;
using UnityEngine;

namespace _Scripts.SO.Actions
{
    [CreateAssetMenu(fileName = "MoveForwardAction", menuName = "PluggableAI/Actions/MoveForwardAction", order = 0)]
    public class MoveForwardAction : Action
    {
        public float speed = 1.0f;
        public override void Act(StateController controller)
        {
            MoveForward(controller);
        }

        private void MoveForward(StateController controller)
        {
            controller.transform.Translate(controller.transform.up * speed * Time.deltaTime);
        }
    }
}