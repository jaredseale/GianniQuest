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
    [SerializeField] GameObject interactableArrow;

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
    [SerializeField] string[] essay;

    [Space(20)]

    Pause pause;
    public float speakerPitch = 1f;
    [SerializeField] bool currentlyTyping = false;
    [SerializeField] bool readingEssay = false;
    [SerializeField] string dialogue;
    [SerializeField] TextMeshProUGUI dialogueText;
    AudioSource myAudioSource;
    [SerializeField] AudioClip[] writingNoises;
    [SerializeField] Queue<string> sentences;
    [SerializeField] Animator essayAnimator;

    void Start() {
        pause = FindObjectOfType<Pause>();
        myCollider = GetComponent<BoxCollider2D>();
        playerController = FindObjectOfType<Player>();
        myAudio = GetComponent<AudioSource>();
        pauser = FindObjectOfType<Pause>();
        myAudioSource = GetComponent<AudioSource>();
        testActive = false;
        sentences = new Queue<string>();

    }
    void Update() {
        OpenTest();
        if (Input.GetMouseButtonDown(0) && pause.gamePaused == false && readingEssay == true) {
            SkipScroll();
        }

        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init2" ||
                PlayerPrefs.GetString("TeacherDialogueState") == "MathTestWrong" ||
                PlayerPrefs.GetString("TeacherDialogueState") == "EnglishTestWrong" ||
                PlayerPrefs.GetString("TeacherDialogueState") == "MathTestDone" ||
                PlayerPrefs.GetString("TeacherDialogueState") == "EnglishTestDone") {
            GetComponent<Interactable>().enabled = true;
        } else {
            GetComponent<Interactable>().enabled = false;
            interactableArrow.SetActive(false);
        }
    }

    void OpenTest() {
        if (Input.GetButtonDown("Up") //if 1) pressed up and 2) on top of loading zone and 3) game not paused and 4) not already taking the test and 5) have talked to the teacher
            && myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))
            && GameObject.Find("Game Manager").GetComponent<Pause>().gamePaused == false
            && testActive == false
            && (PlayerPrefs.GetString("TeacherDialogueState") != "Init" && PlayerPrefs.GetString("TeacherDialogueState") != "LevelComplete")) {

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

        PlayerPrefs.SetString("TeacherDialogueState", "EnglishTestDone");
        GenerateStory();
    }

    public void GenerateStory() {
        //make a string array taking the values from the text fields
        essay = new string[] { MALE_NAME_1.text.ToUpper() + "\'S FIRST DAY AT COLLEGE\na story by Gianni",
            FirstLetterToUpper(NUMBER_1.text) + " years ago, there was a " + ADJECTIVE_1.text.ToLower() + " boy named " + FirstLetterToUpper(MALE_NAME_1.text) + ".",
            FirstLetterToUpper(MALE_NAME_1.text) + " loved to " + VERB_1.text.ToLower() + ", " + VERB_2.text.ToLower() 
                + ", and " + VERB_3.text.ToLower() + ", but above all else, he wanted to be a " + OCCUPATION_1.text.ToLower() + ".",
            "One day, after a rough day of working a " + NUMBER_2.text.ToLower() + " hour shift at " + FirstLetterToUpper(COMPANY.text) + ", "
                + FirstLetterToUpper(MALE_NAME_1.text) + " finally decided to chase his dream.",
            FirstLetterToUpper(MALE_NAME_1.text) + " asked his mother, " + FirstLetterToUpper(FEMALE_NAME_1.text) + ", and his father, "
                + FirstLetterToUpper(MALE_NAME_2.text) + ", for some money so he could go to " + OCCUPATION_1.text.ToLower() + " college.",
            "Being a pair of very wealthy " + OCCUPATION_2.text.ToLower() + "s, " + FirstLetterToUpper(MALE_NAME_1.text) + "\'s parents chose to send him to the "
                + ADJECTIVE_2.text.ToLower() + "est " + OCCUPATION_1.text.ToLower() + " school in the world, " + FirstLetterToUpper(FANCY_SOUNDING_NAME.text) + " University.",
            FirstLetterToUpper(MONTH_OF_THE_YEAR.text) + " rolled around, and it was time for " + FirstLetterToUpper(MALE_NAME_1.text) 
                + " to go to his first class, " + FirstLetterToUpper(WORD_ENDING_IN_OLOGY.text) + " 101.",
            "In his class, Professor " + FirstLetterToUpper(FEMALE_NAME_2.text) + " taught the class all about " + PLURAL_NOUN_1.text.ToLower() + ", "
                + PLURAL_NOUN_2.text.ToLower() + ", and " + PLURAL_NOUN_3.text.ToLower() + ", but " + FirstLetterToUpper(MALE_NAME_1.text)
                + "\'s favorite topic of the day was when she talked about her " + ADJECTIVE_3.text.ToLower() + " " + BODY_PART.text.ToLower() + ".",
            "After the lesson, " + FirstLetterToUpper(MALE_NAME_1.text) + " headed to the cafeteria to eat at his favorite " + FirstLetterToUpper(COUNTRY.text)
                + "-style " + FOOD.text.ToLower() + " restaurant, " + FirstLetterToUpper(ADJECTIVE_4.text) + " " + FirstLetterToUpper(MALE_NAME_3.text) + "\'s.",
            "He ordered a " + ADJECTIVE_5.text.ToLower() + " " + FOOD.text.ToLower() + ", a warm " + FirstLetterToUpper(SODA_BRAND.text)
                + ", and a " + COLOR.text.ToLower() + " " + DESSERT.text.ToLower() + ". Yum!",
            "Satisfied with his new education and tasty meal, " + FirstLetterToUpper(MALE_NAME_1.text) + " rode his " + VEHICLE.text.ToLower()
                + " back home, excited to tell his parents all about his first day at " + FirstLetterToUpper(FANCY_SOUNDING_NAME.text) + " University."};
    }

    public string FirstLetterToUpper(string str) {
        if (str == null)
            return null;

        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();

        return str.ToUpper();
    }

    public void BeginReadingEssay() {
        readingEssay = true;
        pauser.canPause = false;
        FindObjectOfType<DialogueManager>().inDialogue = true;
        FindObjectOfType<Player>().canMove = false;
        essayAnimator.SetTrigger("StartTest");
        sentences.Clear();
        foreach (string sentence in essay) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    void EndDialogue() {
        essayAnimator.SetTrigger("CloseTest");
        myAudio.PlayOneShot(ruffle);
        FindObjectOfType<Player>().canMove = true;
        readingEssay = false;
        pauser.canPause = true;
        FindObjectOfType<DialogueManager>().inDialogue = false;
        PlayerPrefs.SetString("TeacherDialogueState", "PostEssay");
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Peek();
        StopAllCoroutines();
        currentlyTyping = true;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        yield return new WaitForSeconds(1f);
        foreach (char letter in sentence.ToCharArray()) {
            if (!currentlyTyping) {
                break;
            }
            myAudioSource.pitch = speakerPitch + Random.Range(-0.1f, 0.1f);
            myAudioSource.PlayOneShot(writingNoises[Random.Range(0, writingNoises.Length)]);
            dialogueText.text += letter;
            if (letter == '!' || letter == '.' || letter == ',' || letter == '?') {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.03f);

        }

        currentlyTyping = false;
    }

    void SkipScroll() {
        if (!currentlyTyping) {
            sentences.Dequeue();
            DisplayNextSentence();
        } else {
            StopAllCoroutines();
            dialogueText.text = sentences.Peek();
            currentlyTyping = false;
        }


    }

    public void TestMethod() {
        foreach (TMP_InputField answer in answerArray) {
            answer.text = "TEST";
        }
    }

}
