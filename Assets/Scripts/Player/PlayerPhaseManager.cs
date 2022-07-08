using System;
using SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerPhaseManager : MonoBehaviour
    {
        [SerializeField] private GamePhaseSO gamePhaseSo;

        [SerializeField] private Vector3 transitionPosition;

        [SerializeField] private Vector3 initialPosition;

        [Range(0.0f, 1.0f)] [SerializeField] private float speedTransition = 0.1f;
        //member variables
        private bool _inTransition = false;
        
        //reference to components
        private PlayerInput _playerInput;

        private void Awake()
        {
            gamePhaseSo.phaseChanged.AddListener(StartTransition);
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (!_inTransition) return;
            switch (gamePhaseSo.currentPhase)
            {
                case GamePhaseSO.Phase.Shooting:
                    TransitionIn();
                    break;
                case GamePhaseSO.Phase.Spawning:
                    TransitionOut();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TransitionIn()
        {
            if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                _inTransition = false;
                _playerInput.enabled = true;
            }

            transform.position = Vector3.Lerp(transform.position, initialPosition, speedTransition);
        }

        private void TransitionOut()
        {
            if (Vector3.Distance(transform.position, transitionPosition) < 0.1f)
            {
                _inTransition = false;
                _playerInput.enabled = false;
            }
            
            transform.position = Vector3.Lerp(transform.position, transitionPosition, speedTransition);
        }

        private void StartTransition()
        {
            _inTransition = true;
        }
    }
}