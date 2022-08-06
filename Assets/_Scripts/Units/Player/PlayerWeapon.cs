using System.Threading.Tasks;
using _Scripts.Managers;
using _Scripts.Units.Bullet;
using _Scripts.Utilities;
using FishNet.Object;
using SO;
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
        private bool _isShooting;

        private void Start()
        {
            GameManager.OnBeforeStateChanged += OnChangeState;
        }

        public void OnDestroy()
        {
            GameManager.OnAfterStateChanged -= OnChangeState;
        }

        public void OnNetworkSpawn()
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
            if (GameManager.Instance.State == GameState.Spawning) return;
            if (!_isShooting) return;
            
            if (_timer >= fireRateInSeconds)
            {
                _timer = 0.0f;
                Shoot_ServerRpc();
            }
            _timer += Time.fixedDeltaTime;
        }

        private async void OnChangeState(GameState state)
        {
            if (state == GameState.Shooting)
            {
                await Task.Delay(1000);
                _isShooting = true;
                return;
            }

            _isShooting = false;
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