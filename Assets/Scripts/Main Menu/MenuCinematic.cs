using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCinematic : MonoBehaviour
{
    [SerializeField] GameObject delayedTitles = null;
    [SerializeField] float titleDelayTime;
    [SerializeField] GameObject delayedButtons = null;
    [SerializeField] float buttonDelayTime;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Camera camera;
    [SerializeField] Animator cameraAnimator;
    public float holdTimer = 0f;
    float skipThreshold = 1f;
    bool cutsceneSkipped = false;
    [SerializeField] GameObject skipText;
    [SerializeField] GameObject introCSText;
    [SerializeField] GameObject sparkles;
    [SerializeField] Animator openingCinematicAnim;
 

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
            StopAllCoroutines();
            cameraAnimator.enabled = false;
            openingCinematicAnim.enabled = false;
            introCSText.SetActive(false);
            sparkles.SetActive(true);
            camera.transform.position = new Vector3(0f, 0f, -10f);
            camera.orthographicSize = 5f;
            delayedButtons.SetActive(true);
            delayedTitles.SetActive(true);
            audioSource.Stop();
            audioSource.time = buttonDelayTime + 0.3f;
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

    public void ShowIntroCSText() {
        introCSText.SetActive(true);
    }

    public void HideIntroCSText() {
        introCSText.SetActive(false);
    }
    public void EnableSparkles() {
        sparkles.SetActive(true);
    }
}
