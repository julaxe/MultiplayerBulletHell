using DefaultNamespace;
using UnityEngine;

namespace SO
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}