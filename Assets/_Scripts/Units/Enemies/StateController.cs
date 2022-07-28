using System;
using Unity.Netcode;
using UnityEngine;
using Weapon;

namespace _Scripts.Units.Enemies
{
    public class StateController : NetworkBehaviour
    {
        public State currentState;
        public WeaponBehaviour weapon;
        [HideInInspector] public float radius = 0.005f;

        public State remainInState;
        
        [HideInInspector] public float timer = 0.0f;
        private void OnValidate()
        {
            weapon = GetComponent<WeaponBehaviour>();
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        public void TransitionToState(State state)
        {
            if (state != remainInState)
            {
                currentState.OnExitState(this);
                currentState = state;
                currentState.OnEnterState(this);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = currentState.colorGizmos;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}