using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;


namespace _Scripts.Managers
{
    public class ShareManager : NetworkBehaviour
    {
        public static ShareManager Instance { get; private set; }

        [SyncVar] public int countDown = 0;
        private float _timer;
        
        private void OnCountdownFinished()
        {
            PlayersManager.Instance.ChangeMusicToShooting();
            PlayersManager.Instance.SpawnPlayersPrefabs();
            PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer1().Owner, GameState.Shooting);
            if(PlayersManager.Instance.Player2Exists())
                PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Spawning);
        }

        private void Update()
        {
            UpdateCountdownOnServer();
        }

        private void UpdateCountdownOnServer()
        {
            if (!IsServer) return;
            if (countDown == 0) return;

            if (_timer >= 1.0f)
            {
                DecreaseCountdown(1);
                if (countDown == 0)
                {
                    OnCountdownFinished();
                }
                
                _timer = 0.0f;
            }

            _timer += Time.deltaTime;
        }

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            Instance = this;
        }

        public void DecreaseCountdown(int value)
        {
            if (countDown == 0) return;

            countDown -= value;
            if (countDown < 0) countDown = 0;
        }

        public void InitializeCountdown()
        {
            countDown = 4;
        }
        
        
        
    }
}