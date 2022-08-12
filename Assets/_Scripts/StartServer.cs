using System.Collections;
using System.Collections.Generic;
using FishNet;
using UnityEngine;

public class StartServer : MonoBehaviour
{
    private void Start()
    {
        InstanceFinder.ServerManager.StartConnection();
    }
}
