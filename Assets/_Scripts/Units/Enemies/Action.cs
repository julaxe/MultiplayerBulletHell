using UnityEngine;

namespace Enemies
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}