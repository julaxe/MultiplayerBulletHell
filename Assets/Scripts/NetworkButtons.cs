using System;
using System.Collections;
using System.Collections.Generic;
using SO;
using UI;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkButtons : MonoBehaviour
{
    [SerializeField] private CameraBehaviour _cameraBehaviour;
    [SerializeField] private NetworkSO networkSo;
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10,10,300,300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            {
                networkSo.isPlayer1 = true;
            }
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
            if (GUILayout.Button("Client"))
            {
                networkSo.isPlayer1 = false;
                _cameraBehaviour.RotateCamera();
                NetworkManager.Singleton.StartClient();
            }
        }
        GUILayout.EndArea();
    }
   
}
