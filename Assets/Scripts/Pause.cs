﻿using System.Collections;
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

    public bool exiting = false;
    Vector2 tempVelo;

    //DISABLE BEFORE RELEASE
    private bool debugMode = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
    }
    void Update() {
        GamePause();

        if (Input.GetKey(KeyCode.Tab) && Input.GetKeyDown(KeyCode.P)) {
            SewersStateReset();
        }
    }

    public void GamePause() {
        if (Input.GetButtonDown("Pause") && gamePaused == false && canPause) {
            gamePaused = true;
            pauseMenu.SetActive(true);
            locationTitle.SetActive(false);

            if (FindObjectOfType<Player>()) {
                tempVelo = player.GetComponent<Rigidbody2D>().velocity;
            }
            Time.timeScale = 0f; //prevent player movement

            if (player != null) { //prevents an error on the overworld
                player.canMove = false;
            }

            mixer.SetFloat("MusicEQ", 0.05f); //gives the underwater effect
        } else if (Input.GetButtonDown("Pause") && gamePaused && controlsMenu.activeSelf == false && !exiting) {
            ClosePauseMenu();
        }
    }

    public void ClosePauseMenu() {
        gamePaused = false;
        pauseMenu.SetActive(false);

        if (FindObjectOfType<Player>()) {
            player.GetComponent<Rigidbody2D>().velocity = tempVelo;
        }
        
        Time.timeScale = 1f; //return player movement

        if (player != null) { //prevents an error on the overworld
            player.canMove = true;
        }
        
        mixer.SetFloat("MusicEQ", 1f);
    }
    public void ExitToMap() {
        exiting = true;
        pauseMenu.SetActive(false);
        if (FindObjectOfType<Player>()) {
            FindObjectOfType<Player>().canMove = false;
        }
        Time.timeScale = 1f;
        mixer.SetFloat("MusicEQ", 1f);
        FindObjectOfType<LevelLoader>().loadSceneDelay = 4f;
        transition.SetTrigger("levelTransition");
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Overworld", true);

        if (SceneManager.GetActiveScene().name == "Ethereal Ascent") { // only comes into play if for some reason the player wants to leave EA during an EA speedrun
            FindObjectOfType<SpawnPosition>().overworldSpawnPosition = "ascent";
        }
    }

    public void ExitToMainMenu()
    {
        ClosePauseMenu();
        exiting = true;
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

        PlayerPrefs.SetInt("SewersLocationDisplay", 1);
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

        if (!SceneManager.GetActiveScene().name.Contains("Sewers") || PlayerPrefs.GetInt("HasDoubleJump") == 0) {
            mapControls.SetActive(false);
        } else {
            mapControls.SetActive(true);
        }
    }

    public void ReturnToMainPauseMenu() {
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void SewersStateReset() {
        PlayerPrefs.SetString("EtherealAscentEntry", "Open");
        PlayerPrefs.SetString("SewersEntry", "Open");

        PlayerPrefs.SetInt("SewersLocationDisplay", 1);

        PlayerPrefs.SetInt("MaxHealth", 6);
        PlayerPrefs.SetInt("CollectedHC1", 0);
        PlayerPrefs.SetInt("CollectedHC2", 0);

        PlayerPrefs.SetInt("HasDoubleJump", 0);
        PlayerPrefs.SetInt("HasCloner", 0);
        PlayerPrefs.SetInt("HasBomb", 0);
        PlayerPrefs.SetInt("HasGun", 0);

        PlayerPrefs.SetInt("Room3Gate", 0);
        PlayerPrefs.SetInt("Room5Button", 0);
        PlayerPrefs.SetInt("Room5WestWall", 0);
        PlayerPrefs.SetInt("Room5EastWall", 0);
        PlayerPrefs.SetInt("Room8Gate", 0);
        PlayerPrefs.SetInt("Room9EnemyGate", 0);
        PlayerPrefs.SetInt("Room9Target", 0);
        PlayerPrefs.SetInt("Room11Wall", 0);
        PlayerPrefs.SetInt("Room14Target1", 0);
        PlayerPrefs.SetInt("Room14Target2", 0);
        PlayerPrefs.SetInt("LCPWall", 0);

        PlayerPrefs.SetInt("BrokeYellowEgg", 0);
        PlayerPrefs.SetInt("BrokeGreenEgg", 0);
        PlayerPrefs.SetInt("BrokeRedEgg", 0);
        PlayerPrefs.SetInt("BrokeBlueEgg", 0);
        PlayerPrefs.SetInt("BrokeOrangeEgg", 0);
        PlayerPrefs.SetInt("BrokePurpleEgg", 0);

        PlayerPrefs.SetInt("SMRoom9", 0);
        PlayerPrefs.SetInt("SMRoom10", 0);
        PlayerPrefs.SetInt("SMRoom11", 0);
        PlayerPrefs.SetInt("SMRoom12", 0);
        PlayerPrefs.SetInt("SMRoom1314", 0);
        PlayerPrefs.SetInt("SMRoom15", 0);
        PlayerPrefs.SetInt("SMRoom16", 0);
    }

}
