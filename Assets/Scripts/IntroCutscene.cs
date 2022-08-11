using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{

    [SerializeField] Animator transition;
    LevelLoader levelLoader;

    public float holdTimer = 0f;
    public float skipThreshold = 1f;
    bool skipping;

    void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        StartCoroutine("CrossFadeDelay");
        StartCoroutine("LoadDelay");

        holdTimer = skipThreshold;
        skipping = false;
    }

    private void Update() {

        if (Input.GetButton("Jump")) {
            holdTimer -= Time.deltaTime;
            if (holdTimer < 0f && skipping == false) {
                skipping = true;
                StopAllCoroutines();
                transition.SetTrigger("gameStart");
                levelLoader.LoadSceneWithDelay("Gianni's Room", true);
            }
        }
    }

    IEnumerator CrossFadeDelay() {
        yield return new WaitForSeconds(18.0f);
        transition.SetTrigger("gameStart");
    }

    IEnumerator LoadDelay() {
        yield return new WaitForSeconds(18.0f);
        levelLoader.LoadSceneWithDelay("Gianni's Room", true);
    }
}
