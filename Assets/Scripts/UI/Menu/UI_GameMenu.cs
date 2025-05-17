using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameMenu : UI_Panel 
{
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        UISystem.current.CloseCurrentPanel();
    }

    public void Save()
    {

    }

    public void Settings()
    {

    }

    public void ExitToMainMenu()
    {
        isCanClose = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        isCanClose = false;
        Application.Quit();
    }
}