﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNICOTutorial : MonoBehaviour
{

    [SerializeField] GameObject[] tutorialScreens;
    [SerializeField] Button infoButton;
    int currentScreenIndex;

    void Start() {
        if (PlayerPrefs.GetString("SNICOTutorialState") == "Incomplete") {
            infoButton.GetComponent<Animator>().SetTrigger("startFlashing");
        }
    }

    public void StartTutorial() {
        PlayerPrefs.SetString("SNICOTutorialState", "Complete");
        infoButton.GetComponent<Animator>().SetTrigger("stopFlashing");
        tutorialScreens[0].SetActive(true);
        currentScreenIndex = 0;
    }

    public void NextScreen() {
        tutorialScreens[currentScreenIndex + 1].SetActive(true);
        tutorialScreens[currentScreenIndex].SetActive(false);
        currentScreenIndex += 1;
    }

    public void PreviousScreen() {
        tutorialScreens[currentScreenIndex - 1].SetActive(true);
        tutorialScreens[currentScreenIndex].SetActive(false);
        currentScreenIndex -= 1;
    }

    public void CloseTutorial() {
        tutorialScreens[currentScreenIndex].SetActive(false);
    }

}