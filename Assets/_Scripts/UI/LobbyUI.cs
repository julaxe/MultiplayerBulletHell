using System;
using _Scripts.Managers;
using FishNet;
using UnityEngine;
using UnityEngine.UI;


namespace _Scripts.UI
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private Button readyButton;
        [SerializeField] private Image readyImage;
        [SerializeField] private Button startButton;

        private void OnEnable()
        {
            GameManager.OnAfterStateChanged += Initialize;
            PlayersManager.PlayerIsReadyChanged += ChangeReadyColor;
        }

        private void OnDisable()
        {
            GameManager.OnAfterStateChanged -= Initialize;
            PlayersManager.PlayerIsReadyChanged -= ChangeReadyColor;
        }

        private void Initialize(GameState state)
        {
            if (state != GameState.Lobby) return;
            readyButton.onClick.AddListener(() =>
            {
                Managers.Player.Instance.ServerSetIsReady(!Managers.Player.Instance.isReady);
            });
            startButton.onClick.AddListener(() =>
            {
                Debug.Log("Start the game");
            });
        }

        private void ChangeReadyColor(bool value)
        {
            readyImage.color = value ? Color.green : Color.red;
        }

        private void Update()
        {
            if (!InstanceFinder.IsHost) return;
            startButton.interactable = PlayersManager.Instance.PlayersAreReady();
        }
    }
}