using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpeedrunManager : MonoBehaviour
{

    public bool destroyable;
    public bool runGoing;
    public float timer = 0;
    public string speedrunType;
    [SerializeField] TextMeshProUGUI timerText;

    void Start() {
        DontDestroyOnLoad(gameObject);
        runGoing = true;
        destroyable = false;
    }

    void Update() {
        if (runGoing) {
            timer += Time.deltaTime;
            UpdateTimer();
        } else {
            timerText.color = new Color(1f, 0.78f, 0f);
        }
        
        if (destroyable) {
            GameObject gameManager = GameObject.Find("Game Manager");
            gameObject.transform.SetParent(gameManager.transform);
        }
    }

    private void UpdateTimer() {
        int numOfSecs = (int) timer;
        int hours = TimeSpan.FromSeconds(numOfSecs).Hours;
        int minutes = TimeSpan.FromSeconds(numOfSecs).Minutes;
        int seconds = TimeSpan.FromSeconds(numOfSecs).Seconds;

        string hoursStr = hours.ToString();

        string minutesStr;
        if (minutes < 10) {
            minutesStr = "0" + minutes.ToString();
        } else {
            minutesStr = minutes.ToString();
        }

        string secondsStr;
        if (seconds < 10) {
            secondsStr = "0" + seconds.ToString();
        } else {
            secondsStr = seconds.ToString();
        }

        if (hours < 1) {
            timerText.text = minutesStr + ":" + secondsStr;
        } else {
            timerText.text = hoursStr + ":" + minutesStr + ":" + secondsStr;
        }
    }
}
