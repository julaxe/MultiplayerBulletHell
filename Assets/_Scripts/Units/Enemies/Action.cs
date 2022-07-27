using UnityEngine;

namespace _Scripts.Units.Enemies
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}