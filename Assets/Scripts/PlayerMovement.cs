using System;
using SO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerMovement : MonoBehaviour
    { 
        [SerializeField] private PlayerStatsSO playerStatsSo;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform _transform;
        [SerializeField] private ParticleSystem flame;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }
        
        private Vector2 _desiredVelocity;
        

        public void OnMove(InputValue value)
        {
            
            var input = value.Get<Vector2>();
            if (_desiredVelocity.Equals(input)) return;
            
            var direction = input.normalized;
            _desiredVelocity = direction * playerStatsSo.speed;

            UpdateFlame();
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector2.Lerp(rb.velocity, _desiredVelocity, playerStatsSo.acceleration);
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            var velRelation = rb.velocity.x / playerStatsSo.speed;
            var newAngle = playerStatsSo.maxRotation * velRelation;
            
            _transform.localRotation = Quaternion.Euler(-90.0f, -newAngle, 0.0f);
        }

        private void UpdateFlame()
        {
            if (_desiredVelocity.y < 0.0f)
            {
                flame.Stop();
            }
            else
            {
                flame.Play();
            }
        }
    }
}