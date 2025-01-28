using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public void GameResume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PlayerController.IsGamePaused = false;
    }
    public void GamePause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PlayerController.IsGamePaused = true;
    }
    //public void OpenControls()
    //{

    //}
    //public void OpenOptions()
    //{

    //}
    //public void LoadMenu()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    //}
    //public void QuitGame()
    //{
    //    Application.Quit();
    //}
}
