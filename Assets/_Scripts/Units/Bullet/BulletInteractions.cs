using System;
using System.Collections;
using DefaultNamespace;
using SO;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Bullet
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

        public override void OnNetworkSpawn()
        {
            bulletAnimation.StartVFX();
            bulletAnimation.ShowMuzzle();
            StartCoroutine(RangeCoroutine(bulletSo.rangeInSeconds));
        }

        public override void OnNetworkDespawn()
        {
            bulletAnimation.ShowHit();
        }

        private void OnValidate()
        {
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