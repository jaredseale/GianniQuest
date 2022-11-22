using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] AudioClip buttonHoverSound = null;
    public bool gamePaused = false;
    public bool canPause = true;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] AudioMixer mixer;
    AudioSource audioSource;
    [SerializeField] GameObject viewOfField = null;
    [SerializeField] Toggle viewOfFieldToggle = null;
    [SerializeField] Animator transition;
    [SerializeField] GameObject locationTitle;
    Player player;

    [SerializeField] GameObject doubleJumpControls;
    [SerializeField] GameObject clonerControls;
    [SerializeField] GameObject bombControls;
    [SerializeField] GameObject gunControls;
    [SerializeField] GameObject mapControls;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
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
            player.canMove = false;
            mixer.SetFloat("MusicEQ", 0.05f); //gives the underwater effect
        } /*else if (Input.GetButtonDown("Pause") && gamePaused == true && controlsMenu.activeSelf == false) {
            ClosePauseMenu();
        }*/
    }

    public void ClosePauseMenu() {
        gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //return player movement
        player.canMove = true;
        mixer.SetFloat("MusicEQ", 1f);
    }
    public void ExitToMap() {
        pauseMenu.SetActive(false);
        if (FindObjectOfType<Player>()) {
            FindObjectOfType<Player>().canMove = false;
        }
        Time.timeScale = 1f;
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

    public void DisplayControls() {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);

        if (PlayerPrefs.GetInt("HasDoubleJump") == 0) {
            doubleJumpControls.SetActive(false);
        } else {
            doubleJumpControls.SetActive(true);
        }

        if (PlayerPrefs.GetInt("HasCloner") == 0) {
            clonerControls.SetActive(false);
        } else {
            clonerControls.SetActive(true);
        }

        if (PlayerPrefs.GetInt("HasBomb") == 0 || !SceneManager.GetActiveScene().name.Contains("Sewers")) {
            bombControls.SetActive(false);
        } else {
            bombControls.SetActive(true);
        }

        if (PlayerPrefs.GetInt("HasGun") == 0 || !SceneManager.GetActiveScene().name.Contains("Sewers")) {
            gunControls.SetActive(false);
        } else {
            gunControls.SetActive(true);
        }

        if (!SceneManager.GetActiveScene().name.Contains("Sewers")) {
            mapControls.SetActive(false);
        } else {
            mapControls.SetActive(true);
        }
    }

    public void ReturnToMainPauseMenu() {
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

}
