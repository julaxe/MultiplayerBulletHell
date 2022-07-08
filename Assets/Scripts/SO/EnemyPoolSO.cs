using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "EnemyPoolSO", menuName = "Enemy/EnemyPool", order = 0)]
    public class EnemyPoolSO : ScriptableObject
    {
        public EnemySO enemySo;
        public Queue<GameObject> enemyPool;
        public int initialAmountOfEnemies;
        
        public void InitializePool()
        {
            enemyPool = new Queue<GameObject>();
            for (int i = 0; i < initialAmountOfEnemies; i++)
            {
                var newEnemy = SpawnNewEnemy();
                AddEnemy(newEnemy);
            }
        }

        public void AddEnemy(GameObject enemy)
        {
            enemyPool.Enqueue(enemy);
            enemy.SetActive(false);
        }

        private GameObject SpawnNewEnemy()
        {
            var newEnemy = Instantiate(enemySo.enemyPrefab);
            return newEnemy;
        }

        public GameObject GetEnemyFromPool()
        {
            if (enemyPool == null) InitializePool();
            var enemy = enemyPool.Count == 0 ? SpawnNewEnemy() : enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
    }
}