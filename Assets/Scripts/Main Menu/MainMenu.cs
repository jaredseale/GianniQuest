﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{
    
    [SerializeField] AudioClip buttonHoverSound = null;
    [SerializeField] AudioClip gameStartSound = null;
    [SerializeField] float buttonHoverSoundVolume = 0.5f;
    [SerializeField] float gameStartSoundVolume = 0.5f;
    AudioSource audioSource;
    [SerializeField] GameObject optionsWindow = null;
    [SerializeField] GameObject dataWindow = null;
    [SerializeField] GameObject mainButtons = null;
    [SerializeField] Button dataManagementButton;
    [SerializeField] GameObject title;
    LevelLoader levelLoader;
    [SerializeField] Animator transition;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] AudioMixer mixer;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        InitializeVolumes();
        levelLoader = FindObjectOfType<LevelLoader>();
        if (PlayerPrefs.GetString("IntroCutsceneStatus") == "Watched") {
            startText.SetText("continue your journey");
            dataManagementButton.interactable = true;
        } else {
            startText.SetText("begin your journey");
            dataManagementButton.interactable = false;
        }
    }

    public void QuitGame() {
        Debug.Log("Exiting game.");
        Application.Quit();
    }

    public void LoadMainGame() {
        if (!PlayerPrefs.HasKey("SaveExists"))
        {
            InitializeSave();
        }
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 1);
        audioSource.PlayOneShot(gameStartSound, gameStartSoundVolume);
        transition.SetTrigger("gameStart");
        if (PlayerPrefs.GetString("IntroCutsceneStatus") != "Watched") {
            PlayerPrefs.SetString("IntroCutsceneStatus", "Watched");
            levelLoader.LoadSceneWithDelay("Wake Up Cutscene", true);
        } else {
            levelLoader.LoadSceneWithDelay("Gianni's Room", true);
        }
    }

    public void DisplayOptions() {
        mainButtons.SetActive(false);
        optionsWindow.SetActive(true);
    }

    public void DisplayDataManagement() {
        mainButtons.SetActive(false);
        title.SetActive(false);
        dataWindow.SetActive(true);
    }

    public void PlayButtonHoverSound() {
        audioSource.PlayOneShot(buttonHoverSound, buttonHoverSoundVolume);
    }

    private void InitializeVolumes() {
        mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void InitializeSave()
    {
        PlayerPrefs.SetInt("SaveExists", 1);

        PlayerPrefs.SetString("TimeOfDay", "Day");
        PlayerPrefs.SetInt("OverworldInstructions", 0);
        PlayerPrefs.SetString("IntroCutsceneStatus", "Unwatched");

        PlayerPrefs.SetString("SisterDialogueState", "Init");
        PlayerPrefs.SetString("MomDialogueState", "Init");
        PlayerPrefs.SetString("BrotherDialogueState", "Init");
        PlayerPrefs.SetString("DadDialogueState", "Init");
        PlayerPrefs.SetString("TeacherDialogueState", "Init");

        PlayerPrefs.SetInt("Dollars", 0);

        PlayerPrefs.SetString("EtherealAscentEntry", "Closed");
        PlayerPrefs.SetString("SewersEntry", "Closed");
        PlayerPrefs.SetString("RicksEntry", "Closed");
        PlayerPrefs.SetString("LCPEntry", "Open");
        PlayerPrefs.SetString("SchoolEntry", "Open");
        PlayerPrefs.SetString("SNICOEntry", "Open");

        PlayerPrefs.SetString("DateProgress", "Init");
        PlayerPrefs.SetString("LCPSpriteState", "Normal");

        PlayerPrefs.SetString("SNICOTutorialState", "Incomplete");
        PlayerPrefs.SetString("SNICOProgress", "Unstarted");

        PlayerPrefs.SetString("LairryDialogueState", "Init");
        PlayerPrefs.SetString("RicksKey", "Uncollected");
        PlayerPrefs.SetInt("DrinkingGameTries", 0);

        PlayerPrefs.SetString("PizzaGuyState", "Init");
    }
}
