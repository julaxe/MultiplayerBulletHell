using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Enemy/Enemy", order = 0)]
    public class EnemySO : ScriptableObject
    {
        public GameObject enemyPrefab;
    }
}