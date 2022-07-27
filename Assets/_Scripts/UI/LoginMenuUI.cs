using _Scripts.Managers;
using UI;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.UI
{
    public class LoginMenuUI : MonoBehaviour
    {
        [SerializeField] private CameraBehaviour _cameraBehaviour;
        [SerializeField] private bool isTesting;

        private void Start()
        {
            if (!isTesting) return;
            NetworkManager.Singleton.StartHost();
            PlayersManager.Instance.isPlayer1 = true;
        }

        public void HostPressed()
        {
            PlayersManager.Instance.isPlayer1 = true;
            NetworkManager.Singleton.StartHost();
            GameManager.Instance.ChangeState(GameState.Lobby);
        }

        public void LoginPressed()
        {
            PlayersManager.Instance.isPlayer1 = false;
            _cameraBehaviour.RotateCamera();
            NetworkManager.Singleton.StartClient();
            GameManager.Instance.ChangeState(GameState.Lobby);
        }

    }
}
