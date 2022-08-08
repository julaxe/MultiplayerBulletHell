using _Scripts.Managers;
using _Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.SO.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/HideUI")]
    public class HideUIAction : Action
    {
        public override void Act(StateController controller)
        {
            
        }

        public override void OnEnter(StateController controller)
        {
            UIManager.Instance.GetSpawningCanvas().enabled = false;
        }
    }
}