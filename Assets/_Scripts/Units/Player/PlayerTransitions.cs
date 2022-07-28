using System;
using _Scripts.Managers;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Player
{
    public class PlayerTransitions : NetworkBehaviour
    {
        [SerializeField] private Vector3 transitionPositionPlayer1;
        [SerializeField] private Vector3 transitionPositionPlayer2;

        [SerializeField] private Vector3 initialPositionPlayer1;
        [SerializeField] private Vector3 initialPositionPlayer2;
        

        [Range(0.0f, 1.0f)] [SerializeField] private float speedTransition = 0.1f;
        //member variables
        private bool _inTransition = false;


        public override void OnNetworkSpawn()
        {
            if (IsOwner) return;
            enabled = false;
        }

        private void Start()
        {
            GameManager.OnBeforeStateChanged += StartTransition;
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
                LerpTo(initialPositionPlayer1, 0.1f);
            }
            else
            {
                LerpTo(initialPositionPlayer2, 0.1f);
            }
        }

        private void TransitionOut()
        {
            if (PlayersManager.Instance.isPlayer1)
            {
                LerpTo(transitionPositionPlayer1, 0.1f);
            }
            else
            {
                LerpTo(transitionPositionPlayer2, 0.1f);
            }
        }

        private void LerpTo(Vector3 position, float distance)
        {
            if (Vector3.Distance(transform.position, position) < distance)
            {
                _inTransition = false;
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