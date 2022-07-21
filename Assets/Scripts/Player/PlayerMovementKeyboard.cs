using System;
using SO;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerMovementKeyboard : NetworkBehaviour
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
        private float _currentRotation;
        


        private void Start()
        {
            _currentRotation = _transform.localRotation.y;
        }

        // public void OnMove(InputValue value)
        // {
        //     
        //     var input = value.Get<Vector2>();
        //     if (_desiredVelocity.Equals(input)) return;
        //     
        //     _inputDirection = input.normalized;
        //     _desiredVelocity = _inputDirection * playerStatsSo.speed;
        //     if (!IsHost)
        //         _desiredVelocity = _desiredVelocity * -1.0f;
        // }

        
        private void FixedUpdate()
        {
            //rb.velocity = Vector2.Lerp(rb.velocity, _desiredVelocity, playerStatsSo.acceleration);
            //UpdateRotation();
            // ConstraintMovementInScreen();
        }

        // private void UpdateRotation()
        // {
        //     //var velRelation = rb.velocity.x / playerStatsSo.speed;
        //     var desiredAngle = playerStatsSo.maxRotation * _inputDirection.x;
        //     _currentRotation = Mathf.Lerp(_currentRotation, desiredAngle, playerStatsSo.accRotation);
        //     _transform.localRotation = !IsHost ? Quaternion.Euler(90.0f, 180 + _currentRotation, 0.0f) : Quaternion.Euler(-90.0f, -_currentRotation, 0.0f);
        // }
        

        // private void ConstraintMovementInScreen()
        // {
        //     var newPos = _transform.position + rb.velocity * Time.fixedDeltaTime;
        //     if (newPos.x > gameSettingsSo.screenWidth * 0.5f ||
        //         newPos.x < -gameSettingsSo.screenWidth * 0.5f ||
        //         newPos.y > gameSettingsSo.screenHeight * 0.5f ||
        //         newPos.y < -gameSettingsSo.screenHeight * 0.5f)
        //     {
        //         rb.velocity = Vector3.zero;
        //     }
        // }
    }
}