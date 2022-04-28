using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class Test : MonoBehaviour
{

    BoxCollider2D myCollider;
    Player playerController;
    [SerializeField] Animator myAnimator;
    AudioSource myAudio;
    [SerializeField] AudioClip ruffle;
    public bool testActive;
    Pause pauser;
    [SerializeField] TMP_InputField answerTextField;
    public int answer;


    void Start() {
        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init2") {
            GetComponent<Interactable>().enabled = true;
        }

        myCollider = GetComponent<BoxCollider2D>();
        playerController = FindObjectOfType<Player>();
        myAudio = GetComponent<AudioSource>();
        pauser = FindObjectOfType<Pause>();
        testActive = false;

    }
    void Update() {
        OpenTest();
    }

    void OpenTest() {
        if (Input.GetButtonDown("Up") //if 1) pressed up and 2) on top of loading zone and 3) game not paused and 4) not already taking the test and 5) have talked to the teacher
            && myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))
            && GameObject.Find("Game Manager").GetComponent<Pause>().gamePaused == false
            && testActive == false
            && (PlayerPrefs.GetString("TeacherDialogueState") == "Init2" || PlayerPrefs.GetString("TeacherDialogueState") == "TestWrong")) {

            playerController.canMove = false;
            myAnimator.SetTrigger("StartTest");
            myAudio.PlayOneShot(ruffle);
            testActive = true;
            pauser.canPause = false;
        }
    }

    public void CloseTest() {
        playerController.canMove = true;
        myAnimator.SetTrigger("CloseTest");
        myAudio.PlayOneShot(ruffle);
        testActive = false;
        pauser.canPause = true;
        int.TryParse(answerTextField.text.Trim(), out answer);
        if (answer == 0 && answerTextField.text.Trim().Length == 1) {
            PlayerPrefs.SetString("TeacherDialogueState", "TestDone");
        } else {
            PlayerPrefs.SetString("TeacherDialogueState", "TestWrong");
        }
    }

}
