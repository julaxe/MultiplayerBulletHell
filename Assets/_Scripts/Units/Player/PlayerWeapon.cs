using _Scripts.Managers;
using Bullet;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Units.Player
{
    public class PlayerWeapon : NetworkBehaviour
    {
        public WeaponSO weaponSo;
        [SerializeField] private Transform canon;
        [SerializeField] private WeaponSO enemyWeapon;

        [SerializeField] private float fireRateInSeconds;

        private float _timer;
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

        private void FixedUpdate()
        {
            if (!IsOwner) return;
            if (GameManager.Instance.State != GameState.Shooting) return;
            
            if (_timer >= fireRateInSeconds)
            {
                _timer = 0.0f;
                Shoot_ServerRpc();
            }
            _timer += Time.fixedDeltaTime;
        }
        

        [ServerRpc]
        private void Shoot_ServerRpc()
        {
            var bullet = NetworkObjectPool.Instance.GetNetworkObject(weaponSo.bulletSo.bulletPrefab,
                canon.position, canon.rotation);
            bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
            bullet.Spawn();
        }
        
    }
}