using System;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace Enemies
{
    public class EnemyManager : NetworkBehaviour
    {
        [SerializeField] private EnemyPoolSO enemyPoolSo;
        [SerializeField] private GameSettingsSO gameSettingsSo;
        

        private void Update()
        {
            if (!IsHost) return;
            if (Mathf.Abs(transform.position.y) > gameSettingsSo.screenHeight)
            {
                transform.position = new Vector3(0.0f, gameSettingsSo.screenHeight * 0.5f, 0.0f);
                enemyPoolSo.AddEnemy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger");
        }
    }
}