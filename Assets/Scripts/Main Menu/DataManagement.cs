using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManagement : MonoBehaviour
{
    [SerializeField] Animator crossfade;
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject dataMenu;
    [SerializeField] GameObject title;
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
        title.SetActive(true);
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

}
