using System.Collections;
using _Scripts.Managers;
using Bullet;
using DefaultNamespace;
using FishNet.Connection;
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
        public override void OnStartClient()
        {
            base.OnStartClient();
            bulletAnimation.StartVFX();
            bulletAnimation.ShowMuzzle();
            StartCoroutine(RangeCoroutine(bulletSo.rangeInSeconds));
        }

        public override void OnStopNetwork()
        {
            base.OnStopNetwork();
            if(!this.gameObject.scene.isLoaded) return;
            bulletAnimation.ShowHit();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            bulletAnimation = GetComponent<BulletAnimation>();
            bulletMovement = GetComponent<BulletMovement>();
        }


        private void Update()
        {
            if (!IsServer) return;
            if (Mathf.Abs(transform.position.y) > GameManager.Instance.gameSettings.screenHeight)
            {
                //transform.position = new Vector3(0.0f, gameSettingsSo.screenHeight * 0.5f, 0.0f);
                ReturnBulletToPool();
            }
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