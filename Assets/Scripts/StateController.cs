using System;
using DefaultNamespace.Weapon;
using SO;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateController : MonoBehaviour
    {
        public State currentState;
        
        [HideInInspector] public float timer = 0.0f;
        [SerializeField] private WeaponBehaviour weapon;
        private void OnValidate()
        {
            weapon = GetComponent<WeaponBehaviour>();
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        public void Shoot()
        {
            weapon.Fire();
        }
    }
}