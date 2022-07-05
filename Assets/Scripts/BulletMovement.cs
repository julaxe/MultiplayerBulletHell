using System;
using SO;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField] private BulletSO bulletSo;

        private Vector2 _velocity;
        private Vector2 _direction;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
            _velocity = _direction * bulletSo.speed;
        }

        private void FixedUpdate()
        {
            transform.Translate(_velocity * Time.fixedDeltaTime);
        }
    }
}