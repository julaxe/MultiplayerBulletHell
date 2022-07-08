using Bullet;
using SO;
using UnityEngine;

namespace Weapon
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private WeaponSO weaponSo;
        [SerializeField] private Transform canon;

        public void Fire()
        {
            var bullet = weaponSo.bulletPoolSo.GetBulletFromPool();
            bullet.transform.position = canon.position;
            bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
        }
    }
}