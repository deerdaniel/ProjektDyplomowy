using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float time;
    private bool isTimeRunning = false;
    public TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        isTimeRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
      if(isTimeRunning)
        {
            time += Time.deltaTime;
            Display(time);
        }  
    }
    void Display(float time)
    {
        time++;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
