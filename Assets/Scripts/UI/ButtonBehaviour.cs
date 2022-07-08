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
            SpawnEnemy_ServerRpc(NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().OwnerClientId);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnEnemy_ServerRpc(ulong clientId)
        {
            var newEnemy = enemyPool.GetEnemyFromPool();
            float randomX = Random.Range(-gameSettings.screenWidth*0.4f, gameSettings.screenWidth*0.4f);
            newEnemy.transform.position = new Vector3(randomX, -gameSettings.screenHeight*0.5f, 0.0f);
            if (newEnemy.GetComponent<NetworkObject>().IsSpawned) return;
            newEnemy.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
        }

        [ClientRpc]
        private void SpawnEnemy_ClientRpc(ulong clientId)
        {
            
        }
    }
}
