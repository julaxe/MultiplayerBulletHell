using _Scripts.Managers;
using FishNet.Object;
using UnityEngine;

namespace _Scripts.Units.Enemies
{
    public class EnemyController : NetworkBehaviour
    {
        private NetworkObject _networkObject;
        private StateController _stateController; 
        
        [SerializeField] private AudioClip explosionClip;
        
        private void Awake()
        {
            _networkObject = GetComponent<NetworkObject>();
            _stateController = GetComponent<StateController>();
        }
        
        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            enabled = IsServer;
        }

        private void Update()
        {
            if (!IsServer) return;
            if (Mathf.Abs(transform.position.y) > GameManager.Instance.gameSettings.screenHeight)
            {
                //transform.position = new Vector3(0.0f, gameSettingsSo.screenHeight * 0.5f, 0.0f);
                ReturnToBulletPool();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer) return;
            
            PlayExplosionClip();

            if (_stateController.isInvulnerable) return;
            //manage collision (score)
            if (other.CompareTag("Player1")) //bullet from player 1
            {
                ScoreFromServer(10, true);
            }
            else if (other.CompareTag("Player2"))
            {
                ScoreFromServer(10, false);
            }
            
            ReturnToBulletPool();
        }

        [ObserversRpc]
        private void PlayExplosionClip()
        {
            AudioSystem.Instance.PlaySound(explosionClip);
        }
        
        
        private void ScoreFromServer(int value, bool isPlayer1)
        {
            if (isPlayer1)
            {
                PlayersManager.Instance.GetPlayer1().score += value;
            }
            else
            {
                PlayersManager.Instance.GetPlayer2().score += value;
            }
        }

        private void ReturnToBulletPool()
        {
            transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
            DespawnFromServer();
        }
        
        private void DespawnFromServer()
        {
            _networkObject.Despawn();
        }
    }
}