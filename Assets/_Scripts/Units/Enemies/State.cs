using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Units.Enemies
{
    [CreateAssetMenu(fileName = "State", menuName = "PluggableAI/State", order = 0)]
    public class State : ScriptableObject
    {
        public List<Action> actions;
        public List<Transition> transitions;
        public Color colorGizmos;

        public void OnEnterState(StateController controller)
        {
            EnterActions(controller);
        }
        public void UpdateState(StateController controller)
        {
            DoActions(controller);
            DoTransitions(controller);
        }

        public void OnExitState(StateController controller)
        {
            ExitActions(controller);
        }
        private void EnterActions(StateController controller)
        {
            foreach (var action in actions)
            {
                action.OnEnter(controller);
            }
        }
        private void DoActions(StateController controller)
        {
            foreach (var action in actions)
            {
                action.Act(controller);
            }
        }
        
        private void ExitActions(StateController controller)
        {
            foreach (var action in actions)
            {
                action.OnExit(controller);
            }
        }

        private void DoTransitions(StateController controller)
        {
            foreach (var transition in transitions)
            {
                if (transition.decision.Decide(controller))
                {
                    controller.TransitionToState(transition.trueState);
                    return;
                }
                controller.TransitionToState(transition.falseState);
            }
        }
    }
}