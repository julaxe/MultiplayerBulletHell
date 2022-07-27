using _Scripts.Managers;
using Enemies;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.UI
{
    public class ButtonBehaviour : NetworkBehaviour
    {
        [SerializeField] private EnemySO enemySo;

        public void SpawnEnemy()
        {
            SpawnEnemy_ServerRpc(PlayersManager.Instance.isPlayer1);
        }
        

        [ServerRpc(RequireOwnership = false)]
        private void SpawnEnemy_ServerRpc(bool isPlayer1)
        {
            float randomX = Random.Range(-GameManager.Instance.gameSettings.screenWidth*0.4f, GameManager.Instance.gameSettings.screenWidth*0.4f);
            var newEnemy = NetworkObjectPool.Instance.GetNetworkObject(enemySo.enemyPrefab,
                new Vector3(-20.0f,0.0f,0.0f), enemySo.enemyPrefab.transform.rotation);
            newEnemy.GetComponent<EnemyManager>().Initialize(randomX, isPlayer1);
            newEnemy.Spawn();
        }

    }
}
