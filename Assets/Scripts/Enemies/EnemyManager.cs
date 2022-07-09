using System;
using SO;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class EnemyManager : NetworkBehaviour
    {
        [SerializeField] private EnemyPoolSO enemyPoolSo;
        [SerializeField] private GameSettingsSO gameSettingsSo;

        private NetworkObject _networkObject;

        private void Awake()
        {
            _networkObject = GetComponent<NetworkObject>();
        }

        private void Start()
        {
            if (!IsServer)
                enabled = false;
        }

        private void Update()
        {
            if (Mathf.Abs(transform.position.y) > gameSettingsSo.screenHeight)
            {
                //transform.position = new Vector3(0.0f, gameSettingsSo.screenHeight * 0.5f, 0.0f);
                NetworkObjectPool.Singleton.ReturnNetworkObject(_networkObject, enemyPoolSo.enemySo.enemyPrefab);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger");
        }
    }
}