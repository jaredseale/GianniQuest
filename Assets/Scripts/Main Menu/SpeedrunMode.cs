using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SpeedrunMode : MonoBehaviour
{
    [SerializeField] GameObject speedrunObject;
    [SerializeField] MainMenu mainMenu;
    [SerializeField] Animator crossfade;
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject speedrunMenu;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] GameObject backToMenuButton;
    [SerializeField] GameObject speedrunInfoText;
    [SerializeField] GameObject speedrunChoices;

    [Space(20)]

    [SerializeField] GameObject EASpeedrun;
    [SerializeField] GameObject sewersSpeedrun;
    [SerializeField] GameObject fullGameSpeedrun;

    LevelLoader levelLoader;
    AudioSource myAudio;
    SaveInitializer saveInitializer;

    private void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        myAudio = GetComponent<AudioSource>();
        saveInitializer = FindObjectOfType<SaveInitializer>();
    }

    public void Update() {
    }

    public void BackToMainButtons() {
        mainButtons.SetActive(true);
        titleText.SetText("GianniQuest");
        speedrunMenu.SetActive(false);
    }

    public void ShowEAMenu() {
        EASpeedrun.SetActive(true);
        speedrunInfoText.SetActive(false);
        speedrunChoices.SetActive(false);
        backToMenuButton.SetActive(false);
    }

    public void ShowSewersMenu() {
        sewersSpeedrun.SetActive(true);
        speedrunInfoText.SetActive(false);
        speedrunChoices.SetActive(false);
        backToMenuButton.SetActive(false);
    }

    public void ShowFullGameMenu() {
        fullGameSpeedrun.SetActive(true);
        speedrunInfoText.SetActive(false);
        speedrunChoices.SetActive(false);
        backToMenuButton.SetActive(false);
    }

    public void BackFromEA() {
        EASpeedrun.SetActive(false);
        speedrunInfoText.SetActive(true);
        speedrunChoices.SetActive(true);
        backToMenuButton.SetActive(true);
    }

    public void BackFromSewers() {
        sewersSpeedrun.SetActive(false);
        speedrunInfoText.SetActive(true);
        speedrunChoices.SetActive(true);
        backToMenuButton.SetActive(true);
    }

    public void BackFromFullGame() {
        fullGameSpeedrun.SetActive(false);
        speedrunInfoText.SetActive(true);
        speedrunChoices.SetActive(true);
        backToMenuButton.SetActive(true);
    }

    public void StartEASpeedrun() {
        SpawnSpeedrunManager("ea");
        saveInitializer.InitializeSave();

        //data management
        PlayerPrefs.SetInt("SchoolDataManagement", 0);
        PlayerPrefs.SetInt("SNICODataManagement", 0);
        PlayerPrefs.SetInt("LCPDataManagement", 0);

        //broad game flags
        PlayerPrefs.SetString("TimeOfDay", "Night");
        PlayerPrefs.SetInt("OverworldInstructions", 1);
        PlayerPrefs.SetString("IntroCutsceneStatus", "Watched");
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 1);
        PlayerPrefs.SetInt("Dollars", 3);

        //house dialogue
        PlayerPrefs.SetString("SisterDialogueState", "Night");
        PlayerPrefs.SetString("MomDialogueState", "Night");
        PlayerPrefs.SetString("BrotherDialogueState", "Night");
        PlayerPrefs.SetString("DadDialogueState", "Night");

        //overworld map entries
        PlayerPrefs.SetString("EtherealAscentEntry", "Open");
        PlayerPrefs.SetString("SewersEntry", "Closed");
        PlayerPrefs.SetString("RicksEntry", "Open");
        PlayerPrefs.SetString("LCPEntry", "Done");
        PlayerPrefs.SetString("SchoolEntry", "Done");
        PlayerPrefs.SetString("SNICOEntry", "Done");

        //LCP state
        PlayerPrefs.SetString("LCPSpriteState", "Crashed");

        //rick's state
        PlayerPrefs.SetString("RicksKey", "Collected");

        crossfade.SetTrigger("gameStart");
        levelLoader.LoadSceneWithDelay("Ethereal Ascent", true);
        myAudio.Play();
    }

    public void StartSewersSpeedrun() {
        SpawnSpeedrunManager("sewers");
        saveInitializer.InitializeSave();

        //data management
        PlayerPrefs.SetInt("SchoolDataManagement", 0);
        PlayerPrefs.SetInt("SNICODataManagement", 0);
        PlayerPrefs.SetInt("LCPDataManagement", 0);
        PlayerPrefs.SetInt("EADataManagement", 0);

        //broad game flags
        PlayerPrefs.SetString("TimeOfDay", "Night");
        PlayerPrefs.SetInt("OverworldInstructions", 1);
        PlayerPrefs.SetString("IntroCutsceneStatus", "Watched");
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 1);
        PlayerPrefs.SetInt("Dollars", 4);

        //house dialogue
        PlayerPrefs.SetString("SisterDialogueState", "Night");
        PlayerPrefs.SetString("MomDialogueState", "Night");
        PlayerPrefs.SetString("BrotherDialogueState", "Night");
        PlayerPrefs.SetString("DadDialogueState", "Night");

        //overworld map entries
        PlayerPrefs.SetString("EtherealAscentEntry", "Open");
        PlayerPrefs.SetString("SewersEntry", "Open");
        PlayerPrefs.SetString("RicksEntry", "Open");
        PlayerPrefs.SetString("LCPEntry", "Done");
        PlayerPrefs.SetString("SchoolEntry", "Done");
        PlayerPrefs.SetString("SNICOEntry", "Done");

        //LCP state
        PlayerPrefs.SetString("LCPSpriteState", "Crashed");

        //rick's state
        PlayerPrefs.SetString("RicksKey", "Collected");

        //EA state
        PlayerPrefs.SetString("SantaDialogueState", "PostDollar");

        crossfade.SetTrigger("gameStart");
        levelLoader.LoadSceneWithDelay("Sewers 1", true);
        myAudio.Play();
    }

    public void StartFullGameSpeedrun() {
        SpawnSpeedrunManager("fullgame");
        saveInitializer.InitializeSave();

        PlayerPrefs.SetString("IntroCutsceneStatus", "Watched");
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 1);

        crossfade.SetTrigger("gameStart");
        levelLoader.LoadSceneWithDelay("Gianni's Room", true);
        myAudio.Play();
    }

    public void SpawnSpeedrunManager(string type) {
        Instantiate(speedrunObject);
        FindObjectOfType<SpeedrunManager>().speedrunType = type;
    }

}
