using _Scripts.Units.Bullet;
using _Scripts.Utilities;
using FishNet.Object;
using SO;
using UnityEngine;

namespace _Scripts.Units.Weapon
{
    public class WeaponBehaviour : NetworkBehaviour
    {
        [SerializeField] private WeaponSO weaponSo;
        [SerializeField] private Transform canon;

        public void Fire()
        {
            Shoot_ServerRpc();
        }
        
        [ServerRpc]
        private void Shoot_ServerRpc()
        {
            var bullet = NetworkObjectPool.Instance.GetNetworkObject(weaponSo.bulletSo.bulletPrefab,
                canon.position, canon.rotation);
            bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
            //bullet.Spawn();
        }
    }
}