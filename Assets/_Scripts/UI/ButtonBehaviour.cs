using Enemies;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace UI
{
    public class ButtonBehaviour : NetworkBehaviour
    {
        [SerializeField] private EnemySO enemySo;
        [SerializeField] private GameSettingsSO gameSettings;
        [SerializeField] private NetworkSO networkSo;

        public void SpawnEnemy()
        {
            SpawnEnemy_ServerRpc(networkSo.isPlayer1);
        }
        

        [ServerRpc(RequireOwnership = false)]
        private void SpawnEnemy_ServerRpc(bool isPlayer1)
        {
            float randomX = Random.Range(-gameSettings.screenWidth*0.4f, gameSettings.screenWidth*0.4f);
            var newEnemy = NetworkObjectPool.Instance.GetNetworkObject(enemySo.enemyPrefab,
                new Vector3(-20.0f,0.0f,0.0f), enemySo.enemyPrefab.transform.rotation);
            newEnemy.GetComponent<EnemyManager>().Initialize(randomX, isPlayer1);
            newEnemy.Spawn();
        }

    }
}
