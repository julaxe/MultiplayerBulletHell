using System;
using System.Collections;
using DefaultNamespace;
using SO;
using Unity.VisualScripting;
using UnityEngine;

namespace Bullet
{
    public class BulletInteractions : MonoBehaviour
    {
        
        [SerializeField] private BulletSO bulletSo;
        [SerializeField] private BulletAnimation bulletAnimation;
        [SerializeField] private BulletMovement bulletMovement;

        private void OnValidate()
        {
            bulletAnimation = GetComponent<BulletAnimation>();
            bulletMovement = GetComponent<BulletMovement>();
        }

        private void OnEnable()
        {
            StartCoroutine(RangeCoroutine(bulletSo.rangeInSeconds));
        }

        public void OnShoot(Vector2 direction)
        {
            bulletMovement.SetDirection(direction);
        }

        IEnumerator RangeCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            bulletAnimation.DisableBulletAnimation();
        }

        private void OnCollisionEnter(Collision other)
        {
            bulletAnimation.ShowHit();
        }
    }
}