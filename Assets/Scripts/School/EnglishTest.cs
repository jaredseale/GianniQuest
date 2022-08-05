using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnglishTest : MonoBehaviour
{

    BoxCollider2D myCollider;
    Player playerController;
    [SerializeField] Animator myAnimator;
    AudioSource myAudio;
    [SerializeField] AudioClip ruffle;
    public bool testActive;
    Pause pauser;

    [Space(20)]

    [SerializeField] TMP_InputField MALE_NAME_1;
    [SerializeField] TMP_InputField MALE_NAME_2;
    [SerializeField] TMP_InputField MALE_NAME_3;
    [SerializeField] TMP_InputField FEMALE_NAME_1;
    [SerializeField] TMP_InputField FEMALE_NAME_2;
    [SerializeField] TMP_InputField FANCY_SOUNDING_NAME;
    [SerializeField] TMP_InputField PLURAL_NOUN_1;
    [SerializeField] TMP_InputField PLURAL_NOUN_2;
    [SerializeField] TMP_InputField PLURAL_NOUN_3;
    [SerializeField] TMP_InputField VERB_1;
    [SerializeField] TMP_InputField VERB_2;
    [SerializeField] TMP_InputField VERB_3;
    [SerializeField] TMP_InputField ADJECTIVE_1;
    [SerializeField] TMP_InputField ADJECTIVE_2;
    [SerializeField] TMP_InputField ADJECTIVE_3;
    [SerializeField] TMP_InputField ADJECTIVE_4;
    [SerializeField] TMP_InputField ADJECTIVE_5;
    [SerializeField] TMP_InputField OCCUPATION_1;
    [SerializeField] TMP_InputField OCCUPATION_2;
    [SerializeField] TMP_InputField COLOR;
    [SerializeField] TMP_InputField NUMBER_1;
    [SerializeField] TMP_InputField NUMBER_2;
    [SerializeField] TMP_InputField BODY_PART;
    [SerializeField] TMP_InputField VEHICLE;
    [SerializeField] TMP_InputField COMPANY;
    [SerializeField] TMP_InputField COUNTRY;
    [SerializeField] TMP_InputField FOOD;
    [SerializeField] TMP_InputField DESSERT;
    [SerializeField] TMP_InputField MONTH_OF_THE_YEAR;
    [SerializeField] TMP_InputField WORD_ENDING_IN_OLOGY;
    [SerializeField] TMP_InputField SODA_BRAND;

    [Space(20)]

    [SerializeField] TMP_InputField[] answerArray;

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
            && (PlayerPrefs.GetString("TeacherDialogueState") != "Init" || PlayerPrefs.GetString("TeacherDialogueState") != "LevelComplete")) {

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

        foreach (TMP_InputField answer in answerArray) {
            if (answer.text.Trim() == "") { //if there are any blank answers
                PlayerPrefs.SetString("TeacherDialogueState", "EnglishTestWrong");
                return;
            }
        }

        GenerateStory();
    }

    public void GenerateStory() {
        //make a string array taking the values from the text fields
    }

}
