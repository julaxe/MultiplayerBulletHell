using System.Collections.Generic;
using _Scripts.Units.Bullet;
using FishNet.Object;
using SO;
using UnityEngine;

namespace Bullet
{
    public class BulletAnimation : NetworkBehaviour
    {
        [SerializeField] private BulletSO bulletSo;
        [SerializeField] private GameObject muzzle;
        [SerializeField] private GameObject hit;
        [SerializeField] private List<ParticleSystem> trails;
        [SerializeField] private BulletInteractions bulletInteractions;

        private NetworkObject _networkObject;

        protected override void OnValidate()
        {
            bulletInteractions = GetComponent<BulletInteractions>();
        }
        private void Awake()
        {
            _networkObject = GetComponent<NetworkObject>();
        }

        public void ShowHit()
        {
            var hitVFX = Instantiate(hit, transform.position, Quaternion.identity);
            
        }

        
        public void ShowMuzzle()
        {
            var muzzleVFX = Instantiate (muzzle, transform.position, Quaternion.identity);
        }

        public void StartVFX()
        {
            foreach (var trail in trails)
            {
                trail.Play();
            }
        }

        public void DisableBulletAnimation()
        {
            if (!IsServer) return;
            //StopTrails_ClientRpc();
            StartCoroutine(bulletInteractions.DestroyInSeconds(trails[0].main.duration));
        }

        // [ClientRpc]
        // private void StopTrails_ClientRpc()
        // {
        //     foreach (var trail in trails)
        //     {
        //         trail.Stop();
        //     }
        // }
        
        
    }
}