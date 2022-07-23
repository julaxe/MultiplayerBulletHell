using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "State", menuName = "PluggableAI/State", order = 0)]
    public class State : ScriptableObject
    {
        public List<Action> actions;

        public void UpdateState(StateController controller)
        {
            foreach (var action in actions)
            {
                action.Act(controller);
            }
        }
    }
}