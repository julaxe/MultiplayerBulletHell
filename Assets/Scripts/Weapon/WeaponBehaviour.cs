using Bullet;
using SO;
using UnityEngine;

namespace DefaultNamespace.Weapon
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private WeaponSO weaponSo;
        [SerializeField] private Transform canon;


        private void Start()
        {
            if (weaponSo.bulletPoolSo.bulletPool != null) return;
            weaponSo.bulletPoolSo.InitializePool();
        }

        public void Fire()
        {
            var bullet = weaponSo.bulletPoolSo.GetBulletFromPool();
            bullet.transform.position = canon.position;
            bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
        }
    }
}