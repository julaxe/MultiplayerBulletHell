using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Player
{
    public class PlayerTransitions : MonoBehaviour
    {
        [SerializeField] private Vector3 transitionPositionPlayer1;
        [SerializeField] private Vector3 transitionPositionPlayer2;

        [SerializeField] private Vector3 initialPositionPlayer1;
        [SerializeField] private Vector3 initialPositionPlayer2;
        

        [Range(0.0f, 1.0f)] [SerializeField] private float speedTransition = 0.1f;
        //member variables
        private bool _inTransition = false;
        
        //reference to components
        private PlayerInput _playerInput;

        private void Awake()
        {
            GameManager.OnAfterStateChanged += StartTransition;
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (!_inTransition) return;
            switch (GameManager.Instance.State)
            {
                case GameState.Shooting:
                    TransitionIn();
                    break;
                case GameState.Spawning:
                    TransitionOut();
                    break;
                default:
                    break;
            }
        }

        private void TransitionIn()
        {
            if (PlayersManager.Instance.isPlayer1)
            {
                LerpTo(initialPositionPlayer1, 0.1f, true);
            }
            else
            {
                LerpTo(initialPositionPlayer2, 0.1f, true);
            }
        }

        private void TransitionOut()
        {
            if (PlayersManager.Instance.isPlayer1)
            {
                LerpTo(transitionPositionPlayer1, 0.1f, false);
            }
            else
            {
                LerpTo(transitionPositionPlayer2, 0.1f, false);
            }
        }

        private void LerpTo(Vector3 position, float distance, bool playerInput)
        {
            if (Vector3.Distance(transform.position, position) < distance)
            {
                _inTransition = false;
                _playerInput.enabled = playerInput;
            }
            
            transform.position = Vector3.Lerp(transform.position, position, speedTransition);
        }

        private void StartTransition(GameState state)
        {
            if(state == GameState.Shooting || state == GameState.Spawning)
                _inTransition = true;
        }
    }
}