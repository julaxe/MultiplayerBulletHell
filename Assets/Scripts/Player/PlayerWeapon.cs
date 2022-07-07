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
            Shoot_ClientRpc();
        }

        [ClientRpc]
        private void Shoot_ClientRpc()
        {
            var bullet = weaponSo.bulletPoolSo.GetBulletFromPool();
            bullet.transform.position = canon.position;
            if (IsOwner)
            {
                bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
            }
            else
            {
                bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward * -1.0f);
            }
        }
    }
}