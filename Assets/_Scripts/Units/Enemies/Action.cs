using UnityEngine;

namespace _Scripts.Units.Enemies
{
    public abstract class Action : ScriptableObject
    {
        public virtual void OnEnter(StateController controller)
        {
        }

        public abstract void Act(StateController controller);

        public virtual void OnExit(StateController controller)
        {
        }
    }
}