using System;
using System.Linq;
using _Scripts.Utilities;
using SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : PersistentSingleton<GameManager> {
       //events
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;
        public static event Action<PlayState> OnBeforePlayStateChanged;
        public static event Action<PlayState> OnAfterPlayStateChanged;
        
        //scenes
        private readonly string mainMenuScene = "Main_Menu_Scene";
        private readonly string gameScene = "Game_Scene";
        private readonly string stageMenuScene = "Stage_Menu_Scene";
        private readonly string lobbyScene = "Lobby_Scene";
        private readonly string storeScene = "Store_Scene";
        private readonly string creditsScene = "Credits_Scene";
        
        //music
        [SerializeField] private AudioClip lobbyClip;
        [SerializeField] private AudioClip shootingClip;
        
        //states
        public GameState State { get; private set; }
        public PlayState PlayState { get; private set; }

        //settings
        public GameSettingsSO gameSettings;

        void Start()
        {
            ChangeState(GameState.PressStart);
        }

        public void ChangeState(GameState newState) 
        {
            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState) 
            {
                case GameState.PressStart:
                    HandlePressStart();
                    break;
                case GameState.MainMenu:
                    HandleMainMenu();
                    break;
                case GameState.StageMenu:
                    HandleStageMenu();
                    break;
                case GameState.SinglePlayer:
                    HandleSinglePlayer();
                    break;
                case GameState.Multiplayer:
                    HandleMultiplayer();
                    break;
                case GameState.Credits:
                    HandleCredits();
                    break;
                case GameState.Store:
                    HandleStore();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnAfterStateChanged?.Invoke(newState);
        
            Debug.Log($"New state: {newState}");
        }

        public void ChangePlayState(PlayState newState)
        {
            if (State != GameState.SinglePlayer && State != GameState.Multiplayer) return;

            OnBeforePlayStateChanged?.Invoke(newState);
            PlayState = newState;

            switch (newState)
            {
                case PlayState.Lobby:
                    HandleLobby();
                    break;
                case PlayState.Countdown:
                    HandleCountdown();
                    break;
                case PlayState.Shooting:
                    HandleShooting();
                    break;
                case PlayState.Spawning:
                    HandleSpawning();
                    break;
                case PlayState.Win:
                    break;
                case PlayState.Lose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            
            OnAfterPlayStateChanged?.Invoke(newState);
            Debug.Log($"New play state: {newState}");
        }

        #region HandleGameState
        private void HandlePressStart()
        {
            AudioSystem.Instance.PlayMusic(lobbyClip);
        }
        private void HandleMainMenu()
        {
            SceneManager.LoadScene(mainMenuScene);
        }

        private void HandleStageMenu()
        {
            SceneManager.LoadScene(stageMenuScene);
        }

        private void HandleSinglePlayer()
        {
            //
            SceneManager.LoadScene(gameScene);
            //ChangePlayState(PlayState.Countdown);
        }

        private void HandleMultiplayer()
        {
            //
            SceneManager.LoadScene(lobbyScene);
            //ChangePlayState(PlayState.Lobby);
        }

        private void HandleCredits()
        {
            SceneManager.LoadScene(creditsScene);
        }

        private void HandleStore()
        {
            SceneManager.LoadScene(storeScene);
        }

        #endregion
        

        #region HandlePlayState

        private void HandleLobby() 
        {
            UIManager.Instance.ShowLobby();
        }

       
        private void HandleCountdown()
        {
            //empty for now
            UIManager.Instance.ShowCountdown();
            TimerManager.Instance.InitializeCountdown(TimerManager.TimerType.Countdown);
            //NetworkObjectPool.Instance.InitializePool();
            
        }

        public void HandleStartGameAfterCountdown()
        {
            AudioSystem.Instance.PlayMusic(shootingClip);
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

        #endregion
       
    }

    /// <summary>
    /// This is obviously an example and I have no idea what kind of game you're making.
    /// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
    /// </summary>
    [Serializable]
    public enum GameState {
        PressStart = 1,
        MainMenu,
        StageMenu,
        SinglePlayer,
        Multiplayer,
        Credits,
        Store
    }

    public enum PlayState
    {
        Lobby,
        Countdown,
        Shooting,
        Spawning,
        Win,
        Lose,
    }
}