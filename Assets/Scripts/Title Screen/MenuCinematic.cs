using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCinematic : MonoBehaviour
{
    [SerializeField] GameObject delayedTitles = null;
    [SerializeField] float titleDelayTime = 19.5f;
    [SerializeField] GameObject delayedButtons = null;
    [SerializeField] float buttonDelayTime = 25.1f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Camera camera;
    [SerializeField] Animator cameraAnimator;
    public float holdTimer = 0f;
    float skipThreshold = 1f;
    bool cutsceneSkipped = false;
    [SerializeField] GameObject skipText;
 

    private void Awake() {
        DeactivateObjects();
    }


    void Start() {
        StartCoroutine(ActivateButtonsAfterDelay());
        StartCoroutine(ActivateTitlesAfterDelay());
        holdTimer = skipThreshold;

        if (PlayerPrefs.GetInt("DisplayMenuCutsceneSkipText") == 0) {
            skipText.SetActive(false);
        }

    }
    private void Update() {

        if (Input.GetButton("Jump")) {
            holdTimer -= Time.deltaTime;
            if (holdTimer < 0f && cutsceneSkipped == false) {
                StopIntroCutscene();
            }
    }

    void StopIntroCutscene() {
            cameraAnimator.enabled = false;
            camera.transform.position = new Vector3(0f, 0f, -10f);
            camera.orthographicSize = 5f;
            delayedButtons.SetActive(true);
            delayedTitles.SetActive(true);
            audioSource.Stop();
            audioSource.time = 25.5f;
            audioSource.Play();
            cutsceneSkipped = true;
            skipText.SetActive(false);
        }
    }

    private void DeactivateObjects() {
        delayedButtons.SetActive(false);
        delayedTitles.SetActive(false);
    }

    private IEnumerator ActivateButtonsAfterDelay() {

        yield return new WaitForSeconds(buttonDelayTime);
        delayedButtons.SetActive(true);
        skipText.SetActive(false);

    }

    private IEnumerator ActivateTitlesAfterDelay() {

        yield return new WaitForSeconds(titleDelayTime);
        delayedTitles.SetActive(true);

    }
}
