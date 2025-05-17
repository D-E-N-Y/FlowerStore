using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private UISystem uiSystem;
    // [SerializeField] private AudioSystem audioSystem;

    private void Start()
    {
        uiSystem.Initialize();
        DontDestroyOnLoad(uiSystem);

        // audioSystem.Initialize();    

        SceneManager.LoadScene("GameScene");
    }
}
