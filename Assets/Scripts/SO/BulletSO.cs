using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "BulletSO", menuName = "Weapon/Bullet", order = 0)]
    public class BulletSO : ScriptableObject
    {
        public float range;
        public float damage;
        public Sprite sprite;
    }
}