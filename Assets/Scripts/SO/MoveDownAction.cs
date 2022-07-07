using DefaultNamespace;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "MoveDown", menuName = "PluggableAI/Actions/MoveDown", order = 0)]
    public class MoveDownAction : Action
    {
        public float speed = 1.0f;
        public override void Act(StateController controller)
        {
            MoveDown(controller);
        }

        private void MoveDown(StateController controller)
        {
            controller.transform.Translate(controller.transform.up * speed * Time.deltaTime);
        }
    }
}