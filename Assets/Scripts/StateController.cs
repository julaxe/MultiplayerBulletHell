using System;
using SO;
using Unity.Netcode;
using UnityEngine;
using Weapon;

public class StateController : NetworkBehaviour
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
        if(IsOwner)
            currentState.UpdateState(this);
    }

    public void Shoot()
    {
        weapon.Fire();
    }
}