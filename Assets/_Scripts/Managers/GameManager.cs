using System;
using System.Linq;
using _Scripts.Utilities;
using SO;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : StaticInstance<GameManager> {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;


        [SerializeField] private AudioClip lobbyClip;
        [SerializeField] private AudioClip shootingClip;
        public GameState State { get; private set; }

        public GameSettingsSO gameSettings;

        void Start()
        {
            ChangeState(GameState.Lobby);
        }

        public void ChangeState(GameState newState) 
        {
            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState) 
            {
                case GameState.Lobby:
                    HandleLobby();
                    break;
                case GameState.Transition:
                    HandleTransition();
                    break;
                case GameState.ServerPlayMode:
                    HandleServerPlayMode();
                    break;
                case GameState.Shooting:
                    HandleShooting();
                    break;
                case GameState.Spawning:
                    HandleSpawning();
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;

                
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnAfterStateChanged?.Invoke(newState);
        
            Debug.Log($"New state: {newState}");
        }

       


        private void HandleLobby() 
        {
            UIManager.Instance.ShowLobby();
            AudioSystem.Instance.PlayMusic(lobbyClip);
        }

        private void HandleTransition()
        {
            //empty for now
            UIManager.Instance.ShowCountdown();
            TimerManager.Instance.InitializeCountdown(TimerManager.TimerType.Countdown);
            //NetworkObjectPool.Instance.InitializePool();
            
        }

        public void HandleStartGameAfterCountdown()
        {
            AudioSystem.Instance.PlayMusic(shootingClip);
        }
        
        private void HandleServerPlayMode()
        {
            TimerManager.Instance.InitializeCountdown(TimerManager.TimerType.Switch);
        }
        private void HandleShooting()
        {
            UIManager.Instance.ShowShootingUI();
        }

        private void HandleSpawning()
        {
            UIManager.Instance.ShowSpawningUI();
        }
    }

    /// <summary>
    /// This is obviously an example and I have no idea what kind of game you're making.
    /// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
    /// </summary>
    [Serializable]
    public enum GameState {
        Lobby = 1,
        Transition,
        ServerPlayMode,
        Shooting,
        Spawning,
        Win,
        Lose,
    }
}