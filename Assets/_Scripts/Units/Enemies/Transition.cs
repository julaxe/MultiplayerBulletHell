using System;
using UnityEngine;

namespace _Scripts.Units.Enemies
{
    [Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}