using UnityEngine;

namespace _Scripts.Units.Enemies
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(StateController controller);
    }
}