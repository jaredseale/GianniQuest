using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] AudioClip buttonHoverSound = null;
    public bool gamePaused = false;
    public bool canPause = true;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioMixer mixer;
    AudioSource audioSource;
    [SerializeField] GameObject viewOfField = null;
    [SerializeField] Toggle viewOfFieldToggle = null;
    [SerializeField] Animator transition;
    [SerializeField] GameObject locationTitle;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        GamePause();
    }

    public void GamePause() {
        if (Input.GetButtonDown("Pause") && gamePaused == false && canPause) {
            gamePaused = true;
            pauseMenu.SetActive(true);
            locationTitle.SetActive(false);
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
        if (FindObjectOfType<Player>()) {
            FindObjectOfType<Player>().canMove = false;
        }
        Time.timeScale = 1f; //return player movement
        mixer.SetFloat("MusicEQ", 1f);
        FindObjectOfType<LevelLoader>().loadSceneDelay = 4f;
        transition.SetTrigger("levelTransition");
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Overworld", true);
    }

    public void ExitToMainMenu()
    {
        ClosePauseMenu();
        if (FindObjectOfType<Player>()) {
            FindObjectOfType<Player>().canMove = false;
        }

        if (FindObjectOfType<OverworldPlayer>()) {
            FindObjectOfType<OverworldPlayer>().canMove = false;
        }
        Destroy(FindObjectOfType<SpawnPosition>().gameObject);
        transition.SetTrigger("levelTransition");
        FindObjectOfType<LevelLoader>().loadSceneDelay = 4f;
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Main Menu", true);
    }

    public void PlayButtonHoverSound() {
        audioSource.PlayOneShot(buttonHoverSound);
    }

    public void ToggleField() {
        if (viewOfFieldToggle.isOn) {
            viewOfField.SetActive(true);
        } else {
            viewOfField.SetActive(false);
        }
    }

}
