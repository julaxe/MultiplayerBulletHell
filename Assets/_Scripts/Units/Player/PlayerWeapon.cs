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
            GameManager.OnBeforePlayStateChanged += OnChangeState;
            
        }

        public void OnDestroy()
        {
            GameManager.OnBeforePlayStateChanged -= OnChangeState;
        }

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            enabled = IsOwner;

            if (!IsOwner) return;
            
            if (!Managers.Player.Instance.isPlayer1)
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
            if (GameManager.Instance.PlayState == PlayState.Spawning) return;
            if (!_isShooting) return;
            
            if (_timer >= fireRateInSeconds)
            {
                _timer = 0.0f;
                AudioSystem.Instance.PlaySound(weaponSo.clip);
                Shoot_ServerRpc(weaponSo.bulletSo.bulletPrefab);
            }
            _timer += Time.fixedDeltaTime;
        }

        private async void OnChangeState(PlayState state)
        {
            if (state == PlayState.Shooting)
            {
                await Task.Delay(1000);
                _isShooting = true;
                return;
            }

            _isShooting = false;
        }


        [ServerRpc]
        private void Shoot_ServerRpc(GameObject bulletPrefab)
        {
            // var bullet = NetworkObjectPool.Instance.GetNetworkObject(weaponSo.bulletSo.bulletPrefab,
            //     canon.position, canon.rotation);
            var bullet = Instantiate(bulletPrefab, canon.position, canon.rotation);
            bullet.GetComponent<BulletInteractions>().OnShoot(canon.forward);
            Spawn(bullet);
        }
        
    }
}