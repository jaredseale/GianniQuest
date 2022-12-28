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

    public void InitializeSave()
    {
        PlayerPrefs.SetInt("SaveExists", 1);

        //data management
        PlayerPrefs.SetInt("SchoolDataManagement", 1);
        PlayerPrefs.SetInt("SNICODataManagement", 1);
        PlayerPrefs.SetInt("LCPDataManagement", 1);
        PlayerPrefs.SetInt("RicksDataManagement", 1);
        PlayerPrefs.SetInt("EADataManagement", 1);
        PlayerPrefs.SetInt("SewersDataManagement", 1);

        //broad game flags
        PlayerPrefs.SetString("TimeOfDay", "Day");
        PlayerPrefs.SetInt("OverworldInstructions", 0);
        PlayerPrefs.SetString("IntroCutsceneStatus", "Unwatched");
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 0);
        PlayerPrefs.SetInt("Dollars", 0);

        //house dialogue
        PlayerPrefs.SetString("SisterDialogueState", "Init");
        PlayerPrefs.SetString("MomDialogueState", "Init");
        PlayerPrefs.SetString("BrotherDialogueState", "Init");
        PlayerPrefs.SetString("DadDialogueState", "Init");

        //overworld map entries
        PlayerPrefs.SetString("EtherealAscentEntry", "Closed");
        PlayerPrefs.SetString("SewersEntry", "Closed");
        PlayerPrefs.SetString("RicksEntry", "Closed");
        PlayerPrefs.SetString("LCPEntry", "Open");
        PlayerPrefs.SetString("SchoolEntry", "Open");
        PlayerPrefs.SetString("SNICOEntry", "Open");

        //school state
        PlayerPrefs.SetString("TeacherDialogueState", "Init");

        //LCP state
        PlayerPrefs.SetString("DateProgress", "Init");
        PlayerPrefs.SetString("LCPSpriteState", "Normal");

        //SNICO state
        PlayerPrefs.SetString("SNICOTutorialState", "Incomplete");
        PlayerPrefs.SetString("SNICOProgress", "Unstarted");

        //rick's state
        PlayerPrefs.SetString("LairryDialogueState", "Init");
        PlayerPrefs.SetString("RicksKey", "Uncollected");
        PlayerPrefs.SetInt("DrinkingGameTries", 0);

        //EA state
        PlayerPrefs.SetString("SantaDialogueState", "Init");

        //sewers state
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

        //pizza guy state
        PlayerPrefs.SetString("PizzaGuyState", "Init");
    }
}
