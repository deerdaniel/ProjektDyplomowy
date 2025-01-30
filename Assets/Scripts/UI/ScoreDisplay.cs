using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText; // Przypisz to pole do Text w UI

    public void DisplayScores()
    {
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);
        List<PlayerScore> scores = new List<PlayerScore>();

        // Pobieramy wszystkie wyniki z PlayerPrefs
        for (int i = 0; i < scoreCount; i++)
        {
            string playerName = PlayerPrefs.GetString("PlayerName_" + i, "Unknown");
            float playerTime = PlayerPrefs.GetFloat("PlayerTime_" + i, float.MaxValue);
            scores.Add(new PlayerScore(playerName, playerTime));
        }

        // Sortujemy wyniki według czasu (od najlepszych)
        var sortedScores = scores.OrderBy(s => s.time).ToList();

        // Wyświetlamy posortowane wyniki w UI
        scoreText.text = "High Scores:\n";
        foreach (var score in sortedScores)
        {
            scoreText.text += score.playerName + ": " + score.time.ToString("F2") + "s\n";
        }
    }
    public void ResetScores()
    {
        // Resetujemy wszystkie zapisane wyniki
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);

        // Usuwamy każdy zapisany wynik
        for (int i = 0; i < scoreCount; i++)
        {
            PlayerPrefs.DeleteKey("PlayerName_" + i);
            PlayerPrefs.DeleteKey("PlayerTime_" + i);
        }

        // Resetujemy licznik wyników
        PlayerPrefs.SetInt("ScoreCount", 0);

        // Zapisujemy zmiany w PlayerPrefs
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