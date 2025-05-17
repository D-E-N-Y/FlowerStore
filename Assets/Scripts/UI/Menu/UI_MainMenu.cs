using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour 
{
    public void NewGame()
    {
        GameInstance.current.NewGame();
        SceneManager.LoadScene("GameScene");
    }

    public void Continue()
    {
        GameInstance.current.LoadGame();
        SceneManager.LoadScene("GameScene");
    }

    public void Settings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}