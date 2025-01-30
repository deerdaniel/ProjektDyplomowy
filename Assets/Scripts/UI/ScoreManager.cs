using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public void SaveScore()
    {
        string playerName = playerNameInput.text;
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        PlayerPrefs.SetFloat("PlayerTime_" + scoreCount, Timer.time);
        PlayerPrefs.SetString("PlayerName_" + scoreCount, playerName);

        PlayerPrefs.SetInt("ScoreCount", scoreCount + 1);

        PlayerPrefs.Save();
        Debug.Log("Saved Player Name: " + playerName + ", Time: " + Timer.time);
    }
}
