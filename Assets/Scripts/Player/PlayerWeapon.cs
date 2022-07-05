using SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponSO weaponSo;
        [SerializeField] private Transform canon;

        private void OnFire(InputValue value)
        {
            Debug.Log("Fire");
            var bullet = weaponSo.bulletPoolSo.GetBulletFromPool();
            bullet.transform.position = canon.position;
            bullet.GetComponent<BulletMovement>().SetDirection(Vector2.up);
        }
    }
}