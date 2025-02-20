using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;

    public void DisplayScores()
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);
        List<PlayerScore> scores = new List<PlayerScore>();
        
        for (int i = 0; i < scoreCount; i++)
        {
            string playerName = PlayerPrefs.GetString("PlayerName_" + i, "Unknown");
            float playerTime = PlayerPrefs.GetFloat("PlayerTime_" + i, float.MaxValue);
            scores.Add(new PlayerScore(playerName, playerTime));
        }

        var sortedScores = scores.OrderBy(s => s.time).ToList();

        scoreText.text = "High Scores:\n";
        foreach (var score in sortedScores)
        {
            scoreText.text += score.playerName + ": " + score.time.ToString("F2") + "s\n";
        }
    }
    public void ResetScores()
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < scoreCount; i++)
        {
            PlayerPrefs.DeleteKey("PlayerName_" + i);
            PlayerPrefs.DeleteKey("PlayerTime_" + i);
        }

        PlayerPrefs.SetInt("ScoreCount", 0);

        PlayerPrefs.Save();
        DisplayScores();
    }
}

public class PlayerScore
{
    public string playerName;
    public float time;

    public PlayerScore(string name, float time)
    {
        playerName = name;
        this.time = time;
    }
}