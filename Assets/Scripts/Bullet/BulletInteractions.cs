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
            base.OnNetworkSpawn();
            enabled = IsServer;
        }

        private void OnValidate()
        {
            bulletAnimation = GetComponent<BulletAnimation>();
            bulletMovement = GetComponent<BulletMovement>();
        }

        private void OnEnable()
        {
            _rangeCoroutine = StartCoroutine(RangeCoroutine(bulletSo.rangeInSeconds));
        }

        public void OnShoot(Vector3 direction)
        {
            bulletMovement.SetDirection(direction);
            bulletAnimation.StartVFX();
            bulletAnimation.ShowMuzzle();
        }

        IEnumerator RangeCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            bulletAnimation.DisableBulletAnimation();
        }

        private void OnTriggerEnter(Collider other)
        {
            bulletAnimation.ShowHit_ClientRpc();
            ReturnBulletToPool();
        }
        private void ReturnBulletToPool()
        {
            StopCoroutine(_rangeCoroutine);
            _networkObject.Despawn();
            NetworkObjectPool.Singleton.ReturnNetworkObject(_networkObject, bulletSo.bulletPrefab);
        }
        
    }
}