using System;
using System.Collections.Generic;
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

        public void Initialize(float posX, bool isPlayer1)
        {
            if (isPlayer1)
            {
                transform.localRotation = Quaternion.Euler(90.0f,180.0f,0.0f);
                transform.position = new Vector3(posX, -gameSettingsSo.screenHeight*0.5f, 0.0f);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(-90.0f,0.0f,0.0f);
                transform.position = new Vector3(posX, gameSettingsSo.screenHeight*0.5f, 0.0f);
            }
        }
        private void Awake()
        {
            _networkObject = GetComponent<NetworkObject>();
        }
        
        private void Start()
        {
            if (!IsServer)
            {
                enabled = false;
            }
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
            NetworkObjectPool.Singleton.ReturnNetworkObject(_networkObject, enemyPoolSo.enemySo.enemyPrefab);
        }
    }
}