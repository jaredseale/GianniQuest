using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    [SerializeField] TMP_InputField textField;
    [SerializeField] Button signButton;
    [SerializeField] AudioClip wooshSFX;
    Animator myAnim;
    AudioSource myAudio;
    
    //timing for all of this stuff is handled in the animation events

    void Start() {
        myAnim = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("PlayerName")) {
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void ClickSignButton() {
        PlayerPrefs.SetString("PlayerName", textField.text);
        myAnim.SetTrigger("splashEnd");
    }

    public void PlayWoosh(float panValue) {
        myAudio.panStereo = panValue;
        myAudio.PlayOneShot(wooshSFX);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

}
