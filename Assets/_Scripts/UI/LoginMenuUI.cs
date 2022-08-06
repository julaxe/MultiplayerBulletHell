using _Scripts.Managers;
using FishNet;
using UI;
using UnityEngine;

namespace _Scripts.UI
{
    public class LoginMenuUI : MonoBehaviour
    {
        [SerializeField] private CameraBehaviour _cameraBehaviour;
        
        public void HostPressed()
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
            GameManager.Instance.ChangeState(GameState.Lobby);
        }

        public void LoginPressed()
        {
            _cameraBehaviour.RotateCamera();
            InstanceFinder.ClientManager.StartConnection();
            GameManager.Instance.ChangeState(GameState.Lobby);
        }

    }
}
