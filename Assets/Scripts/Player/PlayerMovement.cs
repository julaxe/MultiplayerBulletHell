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
        [SerializeField] private GameSettingsSO gameSettingsSo;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform _transform;
        [SerializeField] private ParticleSystem flame;

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }
        
        private Vector2 _desiredVelocity;
        private Vector2 _inputDirection;
        

        public void OnMove(InputValue value)
        {
            
            var input = value.Get<Vector2>();
            if (_desiredVelocity.Equals(input)) return;
            
            _inputDirection = input.normalized;
            _desiredVelocity = _inputDirection * playerStatsSo.speed;

            UpdateFlame();
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector2.Lerp(rb.velocity, _desiredVelocity, playerStatsSo.acceleration);
            UpdateRotation();
            ConstraintMovementInScreen();
        }

        private void UpdateRotation()
        {
            //var velRelation = rb.velocity.x / playerStatsSo.speed;
            var newAngle = playerStatsSo.maxRotation * _inputDirection.x;
            
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

        private void ConstraintMovementInScreen()
        {
            var newPos = _transform.position + rb.velocity * Time.fixedDeltaTime;
            if (newPos.x > gameSettingsSo.screenWidth * 0.5f ||
                newPos.x < -gameSettingsSo.screenWidth * 0.5f ||
                newPos.y > gameSettingsSo.screenHeight * 0.5f ||
                newPos.y < -gameSettingsSo.screenHeight * 0.5f)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}