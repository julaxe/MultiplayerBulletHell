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
        private Vector2 _direction;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.velocity = velocity;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
            velocity = _direction * bulletSo.speed;
        }
        
    }
}