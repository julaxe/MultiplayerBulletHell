using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderManually : MonoBehaviour
{
    [SerializeField] private string serverSceneName;
    [SerializeField] private string clientSceneName;

    [SerializeField] private Button serverButton;
    [SerializeField] private Button clientButton;

    private void Start()
    {
        serverButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(serverSceneName);
        });
        clientButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(clientSceneName);
        });
    }
}
