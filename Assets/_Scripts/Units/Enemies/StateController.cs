using System;
using Unity.Netcode;
using UnityEngine;
using Weapon;

namespace _Scripts.Units.Enemies
{
    public class StateController : NetworkBehaviour
    {
        //enemy
        public WeaponBehaviour weapon;
        
        //interactable
        public float clickRadius = 0.1f;
        
        //arrowLine
        [NonSerialized] public Line arrowLine;
        [NonSerialized] public Vector3[] arrowLinePositions;
        [NonSerialized] public int currentArrowLinePosition = 0;
        
        //state
        public State currentState;
        public State remainInState;
        
        //helpers
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
            Gizmos.DrawWireSphere(transform.position, clickRadius);
        }
    }
}