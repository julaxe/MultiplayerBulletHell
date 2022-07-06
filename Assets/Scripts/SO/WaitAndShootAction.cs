using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace SO
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
                controller.Shoot();
                controller.timer = 0.0f;
            }

            controller.timer += Time.deltaTime;
        }

        
    }
}