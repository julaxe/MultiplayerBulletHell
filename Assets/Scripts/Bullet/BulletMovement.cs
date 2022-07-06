using System;
using SO;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private BulletSO bulletSo;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector2 velocity;
        private Vector3 _direction;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
            rb.velocity = _direction * bulletSo.speed;
        }
        
    }
}