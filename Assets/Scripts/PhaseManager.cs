using System;
using System.Collections;
using System.Collections.Generic;
using SO;
using Unity.Netcode;
using UnityEngine;

public class PhaseManager : NetworkBehaviour
{
    [SerializeField] private GamePhaseSO gamePhaseSo;
    [SerializeField] private Canvas canvas;
    private void Awake()
    {
       
        gamePhaseSo.phaseChanged.AddListener(UpdatePhase);
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        gamePhaseSo.currentPhase = IsHost ? GamePhaseSO.Phase.Spawning : GamePhaseSO.Phase.Shooting;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10,10,50,50));
        if (GUILayout.Button("Phase")) ChangePhase_ServerRpc();
        GUILayout.EndArea();
    }

    private void UpdatePhase()
    {
        switch (gamePhaseSo.currentPhase)
        {
            case GamePhaseSO.Phase.Shooting:
                canvas.enabled = false;
                break;
            case GamePhaseSO.Phase.Spawning:
                canvas.enabled = true;
                break;
        }
    }
    
    [ServerRpc]
    private void ChangePhase_ServerRpc()
    {
        ChangePhase_ClientRpc();
    }

    [ClientRpc]
    private void ChangePhase_ClientRpc()
    {
        gamePhaseSo.ChangePhase();
    }
    
}
