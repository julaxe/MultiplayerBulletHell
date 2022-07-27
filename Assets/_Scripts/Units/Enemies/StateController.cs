using System;
using Enemies;
using SO;
using Unity.Netcode;
using UnityEngine;
using Weapon;

public class StateController : NetworkBehaviour
{
    public State currentState;
        
    [HideInInspector] public float timer = 0.0f;
    [SerializeField] public WeaponBehaviour weapon;
    [SerializeField] private EnemyNetwork enemyNetwork;
    private void OnValidate()
    {
        enemyNetwork = GetComponent<EnemyNetwork>();
        weapon = GetComponent<WeaponBehaviour>();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }
    
}