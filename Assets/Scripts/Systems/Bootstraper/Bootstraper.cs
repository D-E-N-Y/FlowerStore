using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private GameInstance gameInstance;
    [SerializeField] private UISystem uiSystem;
    // [SerializeField] private AudioSystem audioSystem;

    private void Start()
    {
        gameInstance.Initialize();
        DontDestroyOnLoad(gameInstance);
        
        uiSystem.Initialize();
        DontDestroyOnLoad(uiSystem);

        // audioSystem.Initialize();    

        SceneManager.LoadScene("MainMenuScene");
    }
}
