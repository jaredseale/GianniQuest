using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] AudioClip buttonHoverSound = null;
    [SerializeField] float buttonHoverSoundVolume = 0.5f;
    public bool gamePaused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioMixer mixer;
    AudioSource audioSource;
    [SerializeField] GameObject viewOfField = null;
    [SerializeField] Toggle viewOfFieldToggle = null;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        GamePause();
    }

    public void GamePause() {
        if (Input.GetButtonDown("Pause") && gamePaused == false) {
            gamePaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; //prevent player movement
            mixer.SetFloat("MusicEQ", 0.05f); //gives the underwater effect
        } else if (Input.GetButtonDown("Pause") && gamePaused == true) {
            ClosePauseMenu();
        }
    }

    public void ClosePauseMenu() {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //return player movement
        mixer.SetFloat("MusicEQ", 1f);
    }
    public void ExitToMap() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //return player movement
        mixer.SetFloat("MusicEQ", 1f);
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Overworld", true);
    }

    public void PlayButtonHoverSound() {
        audioSource.PlayOneShot(buttonHoverSound, buttonHoverSoundVolume);
    }

    public void ToggleField() {
        if (viewOfFieldToggle.isOn) {
            viewOfField.SetActive(true);
        } else {
            viewOfField.SetActive(false);
        }
    }
}
