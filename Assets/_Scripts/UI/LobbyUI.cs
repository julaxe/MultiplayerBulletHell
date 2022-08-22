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
            GameManager.OnAfterPlayStateChanged += Initialize;
            PlayersManager.PlayerIsReadyChanged += ChangeReadyColor;
        }

        private void OnDisable()
        {
            GameManager.OnAfterPlayStateChanged -= Initialize;
            PlayersManager.PlayerIsReadyChanged -= ChangeReadyColor;
        }

        private void Initialize(PlayState state)
        {
            if (state != PlayState.Lobby) return;
            readyButton.onClick.AddListener(() =>
            {
                Player.Instance.ServerSetIsReady(!Managers.Player.Instance.isReady);
            });
            startButton.onClick.AddListener(() =>
            {
                Player.Instance.ChangePlayStateForEveryBody(PlayState.Countdown);
            });
        }

        private void ChangeReadyColor(bool value)
        {
            readyImage.color = value ? Color.green : Color.red;
        }

        private void Update()
        {
            if (InstanceFinder.IsServer) return;
            if (Player.Instance == null) return;
            if (!Player.Instance.isPlayer1) return;
            startButton.interactable = PlayersManager.Instance.PlayersAreReady();
        }
    }
}