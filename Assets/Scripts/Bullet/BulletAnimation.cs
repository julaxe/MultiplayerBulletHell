using System;
using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;

namespace Bullet
{
    public class BulletAnimation : MonoBehaviour
    {
        [SerializeField] private BulletPoolSO bulletPoolSo;
        [SerializeField] private GameObject muzzle;
        [SerializeField] private GameObject hit;
        [SerializeField] private List<ParticleSystem> trails;

        public void ShowHit()
        {
            var hitVFX = Instantiate(hit, transform.position, Quaternion.identity);
            DisableBulletAnimation();
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
            foreach (var trail in trails)
            {
                trail.Stop();
            }
            StartCoroutine(DisableInSeconds(trails[0].main.duration));
        }

        IEnumerator DisableInSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            bulletPoolSo.AddBullet(gameObject);
        }

        
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 70, 100, 30), "Hit"))
            {
                ShowHit();
            }
            if (GUI.Button(new Rect(10, 110, 100, 30), "Muzzle"))
            {
                ShowMuzzle();
            }
        }
    }
}