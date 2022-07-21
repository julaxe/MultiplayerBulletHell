using Bullet;
using SO;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerWeapon : NetworkBehaviour
    {
        public WeaponSO weaponSo;
        [SerializeField] private Transform canon;

        [SerializeField] private WeaponSO enemyWeapon;
        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                ChangeToEnemy();
            }
            else
            {
                //is player
                weaponSo.bulletSo.bulletPrefab.tag = "Player1";
            }
        }
        void ChangeToEnemy()
        {
            weaponSo = enemyWeapon;
            weaponSo.bulletSo.bulletPrefab.tag = "Player2";
        }
        
        private void OnFire(InputValue value)
        {
            if (!IsOwner) return;
            Shoot_ServerRpc();
        }

        

        [ServerRpc]
        private void Shoot_ServerRpc()
        {
            var bullet = NetworkObjectPool.Singleton.GetNetworkObject(weaponSo.bulletSo.bulletPrefab,
                canon.position, canon.rotation);
            bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
            bullet.Spawn();
        }
        
    }
}