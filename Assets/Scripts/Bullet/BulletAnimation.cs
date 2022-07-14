using System;
using System.Collections;
using System.Collections.Generic;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace Bullet
{
    public class BulletAnimation : NetworkBehaviour
    {
        [SerializeField] private BulletPoolSO bulletPoolSo;
        [SerializeField] private GameObject muzzle;
        [SerializeField] private GameObject hit;
        [SerializeField] private List<ParticleSystem> trails;

        private NetworkObject _networkObject;

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
            StopTrails_ClientRpc();
            StartCoroutine(DisableInSeconds(trails[0].main.duration));
        }

        [ClientRpc]
        private void StopTrails_ClientRpc()
        {
            foreach (var trail in trails)
            {
                trail.Stop();
            }
        }

        [ClientRpc]
        public void ShowHit_ClientRpc()
        {
            ShowHit();
        }

        IEnumerator DisableInSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _networkObject.Despawn();
            NetworkObjectPool.Singleton.ReturnNetworkObject(_networkObject, bulletPoolSo.bulletSo.bulletPrefab);
        }
        
        

        
        private void OnGUI()
        {
            // if (GUI.Button(new Rect(10, 70, 100, 30), "Hit"))
            // {
            //     ShowHit();
            // }
            // if (GUI.Button(new Rect(10, 110, 100, 30), "Muzzle"))
            // {
            //     ShowMuzzle();
            // }
        }
    }
}