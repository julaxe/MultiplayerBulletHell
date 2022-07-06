using System;
using Bullet;
using SO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponSO weaponSo;
        [SerializeField] private Transform canon;


        private void Start()
        {
            weaponSo.bulletPoolSo.InitializePool();
        }

        private void OnFire(InputValue value)
        {
            Debug.Log("Fire");
            var bullet = weaponSo.bulletPoolSo.GetBulletFromPool();
            bullet.transform.position = canon.position;
            bullet.GetComponent<BulletInteractions>().OnShoot(Vector2.up);
        }
    }
}