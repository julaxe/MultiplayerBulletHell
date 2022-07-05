using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "BulletSO", menuName = "Weapon/Bullet", order = 0)]
    public class BulletSO : ScriptableObject
    {
        public GameObject bulletPrefab;
        public float rangeInSeconds;
        public float damage;
        public float speed;
    }
}