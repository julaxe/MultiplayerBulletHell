using _Scripts.Managers;
using _Scripts.Units.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.SO.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ShowUI")]
    public class ShowUIAction : Action
    {
        public override void Act(StateController controller)
        {
            
        }

        public override void OnEnter(StateController controller)
        {
            if (GameManager.Instance.State == GameState.Shooting) return;
            GameManager.Instance.spawningCanvas.enabled = true;
        }
    }
}