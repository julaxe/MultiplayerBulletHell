using _Scripts.Managers;
using FishNet;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class CountdownUI : MonoBehaviour
    {
        private float _timer;

        [SerializeField] private TextMeshProUGUI text;

        private void OnCountdownFinished()
        {
            PlayersManager.Instance.SpawnPlayersPrefabs();
            PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer1().Owner, GameState.Shooting);
            if(PlayersManager.Instance.Player2Exists())
                PlayersManager.Instance.ChangeStateToSpecificClient(PlayersManager.Instance.GetPlayer2().Owner, GameState.Spawning);
        }

        private void Update()
        {
            UpdateText();
            UpdateCountdownOnServer();
        }

        private void UpdateCountdownOnServer()
        {
            if (!InstanceFinder.IsServer) return;
            if (ShareManager.Instance == null || ShareManager.Instance.countDown == 0) return;

            if (_timer >= 1.0f)
            {
                ShareManager.Instance.DecreaseCountdown(1);
                if (ShareManager.Instance.countDown == 0)
                {
                    OnCountdownFinished();
                }
                
                _timer = 0.0f;
            }

            _timer += Time.deltaTime;
        }
        private void UpdateText()
        {
            if(ShareManager.Instance != null)
                text.text = ShareManager.Instance.countDown.ToString();
        }
    }
}