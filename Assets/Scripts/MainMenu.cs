using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    
    [SerializeField] AudioClip buttonHoverSound = null;
    [SerializeField] AudioClip gameStartSound = null;
    [SerializeField] float buttonHoverSoundVolume = 0.5f;
    [SerializeField] float gameStartSoundVolume = 0.5f;
    AudioSource audioSource;
    [SerializeField] GameObject optionsWindow = null;
    [SerializeField] GameObject mainButtons = null;
    LevelLoader levelLoader;
    [SerializeField] Animator transition;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void QuitGame() {
        Debug.Log("Exiting game.");
        Application.Quit();
    }

    public void LoadMainGame() {
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 1);
        audioSource.PlayOneShot(gameStartSound, gameStartSoundVolume);
        transition.SetTrigger("gameStart");
        if (PlayerPrefs.GetString("IntroCutsceneStatus") == "Unwatched") {
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

    public void PlayButtonHoverSound() {
        audioSource.PlayOneShot(buttonHoverSound, buttonHoverSoundVolume);
    }
}
