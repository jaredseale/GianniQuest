using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldInstructions : MonoBehaviour
{
    Animator myAnim;
    Pause pauseMenu;

    private void Awake() {
        if (PlayerPrefs.GetInt("OverworldInstructions") == 1) {
            gameObject.SetActive(false);
        }
    }

    private void Start() {
        pauseMenu = FindObjectOfType<Pause>();
        pauseMenu.canPause = false;
        myAnim = GetComponent<Animator>();
    }


    public void Open() {
        gameObject.SetActive(true);
    
    }

    public void Close() {
        PlayerPrefs.SetInt("OverworldInstructions", 1);
        myAnim.SetTrigger("CloseInstructions");
        pauseMenu.canPause = true;
    }

    public void HideInstructions() {
        gameObject.SetActive(false);
    }
}
