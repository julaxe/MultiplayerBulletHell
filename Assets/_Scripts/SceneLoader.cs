
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string serverSceneName;
        [SerializeField] private string clientSceneName;
        private void Start()
        {
            if (Application.platform is RuntimePlatform.WindowsServer or RuntimePlatform.OSXServer or RuntimePlatform
                .LinuxServer)
            {
                SceneManager.LoadScene(serverSceneName);
            }
            else
            {
                SceneManager.LoadScene(clientSceneName);
            }
        }
    }
}