using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptions : MonoBehaviour
{

    [SerializeField] GameObject mainButtons = null;
    [SerializeField] GameObject optionsMenu = null;
    [SerializeField] GameObject viewOfField = null;
    [SerializeField] Toggle viewOfFieldToggle = null;


    public void BackToMainButtons() {
        mainButtons.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ToggleField() {
        if (viewOfFieldToggle.isOn) {
            viewOfField.SetActive(true);
        } else {
            viewOfField.SetActive(false);
        }
    }
}
