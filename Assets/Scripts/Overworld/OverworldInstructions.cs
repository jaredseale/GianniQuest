using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldInstructions : MonoBehaviour
{
    Animator myAnim;
    Pause pauseMenu;

    private void Awake() {
        if (PlayerPrefs.GetInt("OverworldInstructions") == 1) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        pauseMenu = FindObjectOfType<Pause>();
        pauseMenu.canPause = false;
        myAnim = GetComponent<Animator>();
    }

    public void Close() {
        PlayerPrefs.SetInt("OverworldInstructions", 1);
        pauseMenu.canPause = true;
        StartCoroutine("CloseInstructions");
    }

    IEnumerator CloseInstructions() {
        myAnim.SetTrigger("CloseInstructions");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
