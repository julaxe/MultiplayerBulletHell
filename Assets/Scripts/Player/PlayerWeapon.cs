using System;
using Bullet;
using SO;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerWeapon : NetworkBehaviour
    {
        public WeaponSO weaponSo;
        [SerializeField] private Transform canon;

        private void OnFire(InputValue value)
        {
            if (!IsOwner) return;
            Shoot_ServerRpc();
        }

        
        
       [ServerRpc]
        private void Shoot_ServerRpc()
        {
            var bullet = NetworkObjectPool.Singleton.GetNetworkObject(weaponSo.bulletPoolSo.bulletSo.bulletPrefab,
                canon.position, canon.rotation);
            bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
            bullet.Spawn();
        }
        
    }
}