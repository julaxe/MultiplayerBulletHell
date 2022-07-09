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

        public void SpawnEnemy()
        {
            SpawnEnemy_ServerRpc(NetworkManager.Singleton.LocalClientId);
        }

        private void SpawnEnemyWithPool()
        {
            
        }

        
        
        
        
        [ServerRpc(RequireOwnership = false)]
        private void SpawnEnemy_ServerRpc(ulong clientId)
        {
            var newEnemy = NetworkObjectPool.Singleton.GetNetworkObject(enemyPool.enemySo.enemyPrefab);
            float randomX = Random.Range(-gameSettings.screenWidth*0.4f, gameSettings.screenWidth*0.4f);
            newEnemy.transform.position = new Vector3(randomX, -gameSettings.screenHeight*0.5f, 0.0f);
            newEnemy.GetComponent<EnemyNetwork>().ownerId = clientId;
            Debug.Log("spawned by: " + clientId);
            if (!newEnemy.IsSpawned)
                newEnemy.Spawn(true);
        }

        [ClientRpc]
        private void SpawnEnemy_ClientRpc(ulong clientId)
        {
            
        }
    }
}
