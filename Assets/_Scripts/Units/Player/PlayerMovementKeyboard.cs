using SO;
using Unity.Netcode;
using UnityEngine;

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
        
    }
}