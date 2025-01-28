using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject Panel;
    public void GameResume()
    {
        MainPanel.SetActive(false);
        Panel.SetActive(false);
        Time.timeScale = 1f;
        PlayerController.IsGamePaused = false;
    }
    public void GamePause()
    {
        MainPanel.SetActive(true);
        
        Time.timeScale = 0f;
        PlayerController.IsGamePaused = true;
    }
    public void LoadMenu()
    {
        GameResume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
