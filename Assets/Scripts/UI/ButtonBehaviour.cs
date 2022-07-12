using Enemies;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace UI
{
    public class ButtonBehaviour : NetworkBehaviour
    {
        [SerializeField] private EnemyPoolSO enemyPool;
        [SerializeField] private GameSettingsSO gameSettings;
        [SerializeField] private NetworkSO networkSo;

        public void SpawnEnemy()
        {
            SpawnEnemy_ServerRpc(networkSo.isPlayer1);
        }

        private void SpawnEnemyWithPool()
        {

        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnEnemy_ServerRpc(bool isPlayer1)
        {
            var newEnemy = NetworkObjectPool.Singleton.GetNetworkObject(enemyPool.enemySo.enemyPrefab,
                new Vector3(-20.0f,0.0f,0.0f), enemyPool.enemySo.enemyPrefab.transform.rotation);
            newEnemy.GetComponent<EnemyNetwork>().Initialize(isPlayer1);
            if (!newEnemy.IsSpawned)
                newEnemy.Spawn();
        }
        
    }
}
