using UnityEngine;
using Enemies;

namespace SO.Actions
{
    [CreateAssetMenu(fileName = "WaitAndShoot", menuName = "PluggableAI/Actions/WaitAndShoot", order = 0)]
    public class WaitAndShootAction : Action
    {
        public float waitTime = 2.0f;
        public override void Act(StateController controller)
        {
            WaitAndShoot(controller);
        }

        private void WaitAndShoot(StateController controller)
        {
            if (controller.timer > waitTime)
            {
                controller.weapon.Fire();
                controller.timer = 0.0f;
            }

            controller.timer += Time.deltaTime;
        }

        
    }
}