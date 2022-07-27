using System;
using SO;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameManager : StaticInstance<GameManager> {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;

        public GameState State { get; private set; }

        public GameSettingsSO gameSettings;
        [SerializeField] private Canvas loginMenuCanvas;
        [SerializeField] private Canvas playerCanvas;
        [SerializeField] private Canvas spawningCanvas;
        
        void Start() => ChangeState(GameState.MainMenu);

        public void ChangeState(GameState newState) 
        {
            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState) 
            {
                case GameState.MainMenu:
                    HandleMainMenu();
                    break;
                case GameState.Lobby:
                    HandleLobby();
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

        private void HandleMainMenu() 
        {
            // Do some start setup, could be environment, cinematics etc

            loginMenuCanvas.enabled = true;
            playerCanvas.enabled = false;
            spawningCanvas.enabled = false;
        }

        private void HandleLobby() 
        {
            loginMenuCanvas.enabled = false;
            playerCanvas.enabled = true;
            spawningCanvas.enabled = false;
            ChangeState(PlayersManager.Instance.isPlayer1 ? GameState.Shooting : GameState.Spawning);
        }

        private void HandleShooting()
        {
            spawningCanvas.enabled = false;
        }

        private void HandleSpawning()
        {
            spawningCanvas.enabled = true;
        }
    }

    /// <summary>
    /// This is obviously an example and I have no idea what kind of game you're making.
    /// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
    /// </summary>
    [Serializable]
    public enum GameState {
        MainMenu = 0,
        Lobby = 1,
        Shooting = 2,
        Spawning = 3,
        Win = 5,
        Lose = 6,
    }
}