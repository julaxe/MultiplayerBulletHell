using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;


namespace _Scripts.Managers
{
    public class TimerManager : NetworkBehaviour
    {
        public enum TimerType
        {
            Countdown,
            Switch
        }
        public static TimerManager Instance { get; private set; }

        [SyncVar] public int countDown = 0;
        private float _timer;
        public TimerType currentType;
        
        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            Instance = this;
        }
        
        private void Update()
        {
            UpdateCountdownOnServer();
        }
        
        private void OnTimerFinished()
        {
            switch (currentType)
            {
                case TimerType.Countdown:
                    OnCountdownFinished();
                    break;
                case TimerType.Switch:
                    OnSwitchFinished();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnSwitchFinished()
        {
            //server
            InitializeCountdown(TimerType.Switch);
            
            //clients
            SwitchClientsGameState();
        }

        private void OnCountdownFinished()
        {
            //server
            GameManager.Instance.ChangeState(GameState.ServerPlayMode);
            
            //clients
            PlayersManager.Instance.ChangeMusicToShooting();
            PlayersManager.Instance.SpawnPlayersPrefabs();
            PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer1().Owner, GameState.Shooting);
            if(PlayersManager.Instance.Player2Exists())
                PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Spawning);
            
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
                    OnTimerFinished();
                }
                
                _timer = 0.0f;
            }

            _timer += Time.deltaTime;
        }
        
        private void DecreaseCountdown(int value)
        {
            if (countDown == 0) return;

            countDown -= value;
            if (countDown < 0) countDown = 0;
        }

        public void InitializeCountdown(TimerType type)
        {
            currentType = type;
            
            switch(currentType)
            {
                case TimerType.Countdown:
                    countDown = 4;
                    break;
                case TimerType.Switch:
                    countDown = 15;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        [ObserversRpc]
        private void SwitchClientsGameState()
        {
            Player.Instance.SwitchBetweenShootingAndSpawning();
        }
        
    }
}