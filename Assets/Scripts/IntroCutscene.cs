using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{

    [SerializeField] Animator transition;
    LevelLoader levelLoader;

    void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        StartCoroutine("CrossFadeDelay");
        StartCoroutine("LoadDelay");
        
    }

    IEnumerator CrossFadeDelay() {
        yield return new WaitForSeconds(18.0f);
        transition.SetTrigger("gameStart");
    }

    IEnumerator LoadDelay() {
        yield return new WaitForSeconds(18.0f);
        levelLoader.LoadSceneWithDelay("Gianni's Room", false);
    }
}
