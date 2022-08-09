using _Scripts.Managers;
using FishNet;
using UnityEngine;

namespace _Scripts.UI
{
    public class LoginMenuUI : MonoBehaviour
    {
        public void HostPressed()
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
            GameManager.Instance.ChangeState(GameState.Lobby);
        }

        public void LoginPressed()
        {
            InstanceFinder.ClientManager.StartConnection();
        }

    }
}
