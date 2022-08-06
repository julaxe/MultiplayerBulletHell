using _Scripts.Managers;
using _Scripts.Utilities;
using SO;
using UnityEngine;

namespace _Scripts.UI
{
    public class ButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private EnemySO enemySo;

        public void SpawnEnemy()
        {
            //SpawnEnemy_ServerRpc(PlayersManager.Instance.isPlayer1, OwnerId);
        }
        
        
        private void SpawnEnemy_ServerRpc(bool isPlayer1, int clientId)
        {
            //float randomX = Random.Range(-GameManager.Instance.gameSettings.screenWidth*0.4f, GameManager.Instance.gameSettings.screenWidth*0.4f);
            var newEnemy = NetworkObjectPool.Instance.GetNetworkObject(enemySo.enemyPrefab,
                new Vector3(-10.0f,0.0f,0.0f), enemySo.enemyPrefab.transform.rotation);
            //newEnemy.GetComponent<EnemyNetwork>().Initialize(randomX, isPlayer1);
            //newEnemy.Spawn(clientId);
        }

    }
}
