using FishNet;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientLoader : MonoBehaviour
{
    private readonly string pressStartScene = "PressStart_Scene";
    // Start is called before the first frame update
    void Start()
    {
        if (InstanceFinder.IsServer) return;

        SceneManager.LoadScene(pressStartScene);

    }
}
