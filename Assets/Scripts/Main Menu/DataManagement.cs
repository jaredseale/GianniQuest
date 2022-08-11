using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DataManagement : MonoBehaviour
{
    [SerializeField] Animator crossfade;
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject dataMenu;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] GameObject levelSkipGroup;
    [SerializeField] GameObject backToMenuButton;
    [SerializeField] GameObject resetSaveButton;
    [SerializeField] GameObject resetSaveConfirmationMenu;

    [Space(20)]

    [SerializeField] Toggle HSHToggle;
    [SerializeField] Toggle SNICOToggle;
    [SerializeField] Toggle LCPToggle;
    [SerializeField] Toggle RicksToggle;
    [SerializeField] Toggle EAToggle;
    [SerializeField] Toggle SewersToggle;

    [Space(20)]

    [SerializeField] GameObject RicksLabel;
    [SerializeField] GameObject EALabel;
    [SerializeField] GameObject SewersLabel;

    private void Start() {
        if (PlayerPrefs.GetInt("SchoolDataManagement") == 0) {
            HSHToggle.interactable = false;
        }

        if (PlayerPrefs.GetInt("SNICODataManagement") == 0) {
            SNICOToggle.interactable = false;
        }

        if (PlayerPrefs.GetInt("LCPDataManagement") == 0) {
            LCPToggle.interactable = false;
        }

        if (PlayerPrefs.GetInt("SchoolDataManagement") == 0
            && PlayerPrefs.GetInt("SNICODataManagement") == 0
            && PlayerPrefs.GetInt("LCPDataManagement") == 0) {
            RicksLabel.SetActive(true);
            }

        if (PlayerPrefs.GetInt("RicksDataManagement") == 0 
            && PlayerPrefs.GetString("RicksKey") == "Collected") {
            RicksToggle.interactable = false;
            EALabel.SetActive(true);
        }

        if (PlayerPrefs.GetInt("EADataManagement") == 0) {
            EAToggle.interactable = false;
        }

        if (PlayerPrefs.GetInt("SewersDataManagement") == 0) {
            SewersToggle.interactable = false;
        }
    }

    public void Update() {
        if (HSHToggle.isOn && SNICOToggle.isOn && LCPToggle.isOn) {
            RicksLabel.SetActive(true);
        }

        if (RicksToggle.isOn) {
            EALabel.SetActive(true);
        }

        if (EAToggle.isOn) {
            SewersLabel.SetActive(true);
        }
    }

    public void BackToMainButtons() {
        mainButtons.SetActive(true);
        titleText.SetText("GianniQuest");
        dataMenu.SetActive(false);
    }

    public void BackToSaveManagement() {
        levelSkipGroup.SetActive(true);
        backToMenuButton.SetActive(true);
        resetSaveButton.SetActive(true);
        resetSaveConfirmationMenu.SetActive(false);
    }

    public void ArmResetSave() {
        levelSkipGroup.SetActive(false);
        backToMenuButton.SetActive(false);
        resetSaveButton.SetActive(false);
        resetSaveConfirmationMenu.SetActive(true);
    }

    public void ResetSave() {
        crossfade.SetTrigger("gameStart");
        FindObjectOfType<MainMenu>().InitializeSave();
        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.SetInt("DisplayMenuCutsceneSkipText", 0);
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Splash Screen", true);
    }

    public void CompleteSchool() {
        PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + 1);
        PlayerPrefs.SetString("SchoolEntry", "Done");
        PlayerPrefs.SetString("MomDialogueState", "PostDollar");
        PlayerPrefs.SetInt("SchoolDataManagement", 0);

        if (PlayerPrefs.GetInt("Dollars") == 3) {
            ChangeToNightState();
        }

        HSHToggle.interactable = false;
    }

    public void CompleteSNICO() {
        PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + 1);
        PlayerPrefs.SetString("SNICOEntry", "Done");
        PlayerPrefs.SetString("DadDialogueState", "PostDollar");
        PlayerPrefs.SetInt("SNICODataManagement", 0);

        if (PlayerPrefs.GetInt("Dollars") == 3) {
            ChangeToNightState();
        }

        SNICOToggle.interactable = false;
    }

    public void CompleteLCP() {
        PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + 1);
        PlayerPrefs.SetString("LCPEntry", "Done");
        PlayerPrefs.SetString("LCPSpriteState", "Crashed");
        PlayerPrefs.SetString("SisterDialogueState", "PostDollar");
        PlayerPrefs.SetInt("LCPDataManagement", 0);

        if (PlayerPrefs.GetInt("Dollars") == 3) {
            ChangeToNightState();
        }

        LCPToggle.interactable = false;
    }

    void ChangeToNightState() {
        PlayerPrefs.SetString("TimeOfDay", "Night");
        PlayerPrefs.SetString("SisterDialogueState", "Night");
        PlayerPrefs.SetString("BrotherDialogueState", "Night");
        PlayerPrefs.SetString("MomDialogueState", "Night");
        PlayerPrefs.SetString("DadDialogueState", "Night");
        PlayerPrefs.SetString("RicksEntry", "Open");
    }

    public void CompleteRicks() {
        PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + 1);
        PlayerPrefs.SetString("LairryDialogueState", "PostDollar");
        PlayerPrefs.SetInt("RicksDataManagement", 0);
        PlayerPrefs.SetString("RicksKey", "Collected");

        RicksToggle.interactable = false;
    }

    public void CompleteEA() {
        //need to make sure key is collected
        //santa is gone
        //toys are collected
        //increase dollar
    }

    public void CompleteSewers() {
        //close ricks, EA, sewers
        //increase dollar
        //give gianni all the equipment(?)
        
    }

}
