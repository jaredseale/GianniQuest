using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    LevelLoader levelLoader;
    AudioSource myAudio;
    [SerializeField] Animator crossfade;

    [SerializeField] GameObject text1;
    [SerializeField] GameObject text2;
    [SerializeField] GameObject text3;
    [SerializeField] GameObject text4;
    [SerializeField] GameObject scrollingCredits;
    [SerializeField] TextMeshProUGUI playerName;

    void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();

        if (FindObjectOfType<SpeedrunManager>()) {
            FindObjectOfType<SpeedrunManager>().destroyable = true;
        }

        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public void EnableText1() {
        text1.SetActive(true);
    }
    public void EnableText2() {
        text2.SetActive(true);
    }
    public void EnableText3() {
        text3.SetActive(true);
    }
    public void EnableText4() {
        text4.SetActive(true);
    }

    public void DisableOpeningTexts() {
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
    }

    public void StartScroll() {
        scrollingCredits.SetActive(true);
    }

    public void LoadMainMenu() {
        levelLoader.LoadSceneWithDelay("Main Menu", true);
        crossfade.SetTrigger("gameStart");
    }

}
