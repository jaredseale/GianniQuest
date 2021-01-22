using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCinematic : MonoBehaviour
{
    [SerializeField] GameObject delayedTitles = null;
    [SerializeField] float titleDelayTime = 19.5f;
    [SerializeField] GameObject delayedButtons = null;
    [SerializeField] float buttonDelayTime = 25.1f;
    


    private void Awake() {
        DeactivateObjects();
    }


    void Start() {
        StartCoroutine(ActivateButtonsAfterDelay());
        StartCoroutine(ActivateTitlesAfterDelay());

    }

    private void DeactivateObjects() {
        delayedButtons.SetActive(false);
        delayedTitles.SetActive(false);
    }

    private IEnumerator ActivateButtonsAfterDelay() {

        yield return new WaitForSeconds(buttonDelayTime);
        delayedButtons.SetActive(true);

    }

    private IEnumerator ActivateTitlesAfterDelay() {

        yield return new WaitForSeconds(titleDelayTime);
        delayedTitles.SetActive(true);

    }
}
