using _Scripts.Managers;
using FishNet.Object;
using UnityEngine;


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


        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            if (!IsOwner)
            {
                enabled = false;
                return;
            }
            
            GameManager.OnBeforeStateChanged += StartTransition;
            if(!Managers.Player.Instance.isPlayer1) RotatePlayer2();
            
            
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

        private void RotatePlayer2()
        {
            transform.rotation = Quaternion.Euler(90,180,0.0f);
        }
        private void TransitionIn()
        {
            if (Managers.Player.Instance.isPlayer1)
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
            if (Managers.Player.Instance.isPlayer1)
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