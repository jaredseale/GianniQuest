using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{
    
    [SerializeField] AudioClip buttonHoverSound = null;
    [SerializeField] AudioClip gameStartSound = null;
    [SerializeField] float gameStartSoundVolume = 0.5f;
    AudioSource audioSource;
    [SerializeField] GameObject optionsWindow = null;
    [SerializeField] GameObject dataWindow = null;
    [SerializeField] GameObject speedrunWindow = null;
    [SerializeField] GameObject mainButtons = null;
    [SerializeField] Button dataManagementButton;
    [SerializeField] GameObject title;
    LevelLoader levelLoader;
    [SerializeField] Animator transition;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Button speedrunButton;
    [SerializeField] TextMeshProUGUI speedrunText;
    [SerializeField] Button creditsButton;
    [SerializeField] TextMeshProUGUI creditsText;


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

        if (PlayerPrefs.GetInt("GameBeaten") != 1) {
            speedrunButton.interactable = false;
            creditsButton.interactable = false;
            speedrunText.text = "???";
            creditsText.text = "???";
        }
    }

    public void QuitGame() {
        Debug.Log("Exiting game.");
        Application.Quit();
    }

    public void LoadMainGame() {
        if (!PlayerPrefs.HasKey("SaveExists"))
        {
            FindObjectOfType<SaveInitializer>().InitializeSave();
        }
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 1);
        audioSource.PlayOneShot(gameStartSound, gameStartSoundVolume);
        transition.SetTrigger("gameStart");

        if (FindObjectOfType<SpawnPosition>()) { //if starting from credits
            var spawnPos = FindObjectOfType<SpawnPosition>();
            Destroy(spawnPos.gameObject);
        }

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
        titleText.SetText("");
        dataWindow.SetActive(true);
    }

    public void DisplaySpeedrunMode() {
        mainButtons.SetActive(false);
        titleText.SetText("");
        speedrunWindow.SetActive(true);
    }

    public void PlayButtonHoverSound() {
        audioSource.PlayOneShot(buttonHoverSound);
    }

    private void InitializeVolumes() {
        mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void PlayCredits() {
        transition.SetTrigger("gameStart");
        levelLoader.LoadSceneWithDelay("Credits", true);
    }

}
