using System.Collections;
using Bullet;
using DefaultNamespace;
using FishNet.Object;
using SO;
using UnityEngine;

namespace _Scripts.Units.Bullet
{
    public class BulletInteractions : NetworkBehaviour
    {
        
        [SerializeField] private BulletSO bulletSo;
        [SerializeField] private BulletAnimation bulletAnimation;
        [SerializeField] private BulletMovement bulletMovement;

        private NetworkObject _networkObject;
        private Coroutine _rangeCoroutine;

        private void Awake()
        {
            _networkObject = GetComponent<NetworkObject>();
        }

        public void OnNetworkSpawn()
        {
            bulletAnimation.StartVFX();
            bulletAnimation.ShowMuzzle();
            StartCoroutine(RangeCoroutine(bulletSo.rangeInSeconds));
        }

        public  void OnNetworkDespawn()
        {
            bulletAnimation.ShowHit();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            bulletAnimation = GetComponent<BulletAnimation>();
            bulletMovement = GetComponent<BulletMovement>();
        }
        

        public void OnShoot(Vector3 direction)
        {
            bulletMovement.SetDirection(direction);
            
        }

        IEnumerator RangeCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            bulletAnimation.DisableBulletAnimation();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;
            
            ReturnBulletToPool();
        }
        private void ReturnBulletToPool()
        {
            _networkObject.Despawn();
        }

       

        public IEnumerator DestroyInSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _networkObject.Despawn();
        }
        
        
    }
}