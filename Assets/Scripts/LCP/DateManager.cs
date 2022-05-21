using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class DateManager : MonoBehaviour
{
    [SerializeField] State AState;
    [SerializeField] State BState;
    [SerializeField] State CState;
    [SerializeField] State GState;
    public State currentState;

    [SerializeField] string dialogue;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] SpriteRenderer characterSprite;
    [SerializeField] SpriteRenderer backgroundSprite;
    [SerializeField] string[] choices;
    [SerializeField] string methodToExecute;

    public float speakerPitch = 1f;
    public bool actionTrigger = false;
    [SerializeField] bool currentlyTyping = false; //this variable allows the audio to stop playing when the text box is clicked through

    [SerializeField] bool choiceActive = false;
    [SerializeField] string currentCharacter;
    [SerializeField] bool characterChange = false;
    [SerializeField] bool musicChange = false;
    [SerializeField] int choiceNumber = 0;

    [SerializeField] AudioSource myMusic;
    [SerializeField] AudioSource mySFX;
    [SerializeField] AudioSource myCharVoice;

    [SerializeField] Animator crossfade;
    [SerializeField] Animator characterAnimator;

    [SerializeField] GameObject choiceButton1;
    [SerializeField] TextMeshProUGUI choice1Text;
    [SerializeField] GameObject choiceButton2;
    [SerializeField] TextMeshProUGUI choice2Text;

    Pause pause;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject characterNameBox;
    [SerializeField] GameObject choiceBoxes;

    void Start() {

        Scene scene = SceneManager.GetActiveScene();
        pause = FindObjectOfType<Pause>();

        //you come in loading different scenes from the overworld based on your progress
        switch (scene.name) {
            case "Le Cul Puant Exterior":
                currentState = AState;
                break;
            case "Le Cul Puant Interior":
                currentState = BState;
                break;
            case "Spaceship":
                currentState = CState;
                break;
            case "Crashed Exterior":
                currentState = GState;
                break;
        } //fill out the rest of these with other checkpoints

        LoadNextState();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && pause.gamePaused == false) {
            SkipScroll();
        }

        if (pause.gamePaused) {
            dialogueBox.SetActive(false);
            choiceBoxes.SetActive(false);
        } else {
            dialogueBox.SetActive(true);
            choiceBoxes.SetActive(true);
        }

        if (currentState.characterName == "") {
            characterNameBox.SetActive(false);
        } else {
            characterNameBox.SetActive(true);
        }

    }

    void LoadNextState() {

        //if (currentState.nextStates.Length == 0 && SceneManager.GetActiveScene().name == "") {
         //   crossfade.SetTrigger("levelDone");
          //  FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Overworld", true);
           // return;
        //}

        if (choiceActive == false) {
            ChangeCharacter(0);
            ChangeMusic(0);
            currentState = currentState.nextStates[0];
        } else {
            if (choiceNumber == 1) {
                ChangeCharacter(0);
                ChangeMusic(0);
                currentState = currentState.nextStates[0];
            } else if (choiceNumber == 2) {
                ChangeCharacter(1);
                ChangeMusic(1);
                currentState = currentState.nextStates[1];
            }
        }

        choices = currentState.choices;

        if (choices.Length > 0) {
            choiceActive = true;
        } else {
            choiceActive = false;
            choiceButton1.SetActive(false);
            choiceButton2.SetActive(false);
        }

        dialogue = currentState.dialogueText;

        if (characterChange) {
            StartCoroutine("CharacterChangeAnimation");
        } else {
            characterSprite.sprite = currentState.characterSprite;
        }

        characterName.SetText(currentState.characterName);

        backgroundSprite.sprite = currentState.backgroundSprite;

        //if there is a change between this and the previous state
        if (musicChange) {
            myMusic.clip = currentState.backgroundMusic; //come back here and adjust for fade out and whatnot
            myMusic.Play();
        }

        mySFX.clip = currentState.SFX;
        mySFX.Play();

        myCharVoice.clip = currentState.characterVoice;

        methodToExecute = currentState.executeMethodAfterDialogue;

        characterChange = false; //just to reset
        musicChange = false;
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        StopCoroutine("TypeSentence");
        currentlyTyping = true;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence() {
        dialogueText.SetText("");
        choice1Text.SetText("");
        choice2Text.SetText("");
        foreach (char letter in dialogue.ToCharArray()) {
            if (!currentlyTyping) {
                break;
            }
            myCharVoice.pitch = speakerPitch + Random.Range(-0.1f, 0.1f);
            myCharVoice.PlayOneShot(myCharVoice.clip);
            dialogueText.text += letter;
            if (letter == '!' || letter == '.' || letter == ',' || letter == '?') {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.03f);

        }

        if (choiceActive) {

            yield return new WaitForSeconds(.5f);
            choiceButton1.SetActive(true);
            foreach (char letter in choices[0].ToCharArray()) {
                if (!currentlyTyping) {
                    break;
                }
                choice1Text.text += letter;
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(.5f);
            choiceButton2.SetActive(true);
            foreach (char letter in choices[1].ToCharArray()) {
                if (!currentlyTyping) {
                    break;
                }
                choice2Text.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
        }

        currentlyTyping = false;
    }

    void SkipScroll() {
        if (!currentlyTyping && !choiceActive) {
            FunctionList(methodToExecute);
            LoadNextState();
        } else {
            StopAllCoroutines();
            if (choiceActive) {
                choiceButton1.SetActive(true);
                choiceButton2.SetActive(true);
                choice1Text.SetText(choices[0]);
                choice2Text.SetText(choices[1]);
            }
            dialogueText.SetText(dialogue);
            currentlyTyping = false;
        }


    }

    void FunctionList(string functionName) {
        switch (functionName) {
            case "LoadInterior":
                LoadInterior();
                break;

            case "LoadSpaceship":
                LoadSpaceship();
                break;

            case "LoadCrashedExterior":
                LoadCrashedExterior();
                break;

            case "FinishLevel":
                FinishLevel();
                break;

            default:
                break;
        }
    }

    public void Choice1() {
        choiceNumber = 1;
        LoadNextState();
    }

    public void Choice2() {
        choiceNumber = 2;
        LoadNextState();
    }

    private void ChangeCharacter(int index) {
        if (currentState.characterName != currentState.nextStates[index].characterName) {
            characterChange = true;
        } else {
            characterChange = false;
        }
    }
    private void ChangeMusic(int index) {
        if (currentState.backgroundMusic != currentState.nextStates[index].backgroundMusic) {
            musicChange = true;
        } else {
            musicChange = false;
        }
    }

    IEnumerator CharacterChangeAnimation() {
        characterAnimator.SetTrigger("changeChar");
        yield return new WaitForSeconds(0.25f);
        characterSprite.sprite = currentState.characterSprite;
        
    }

    private void LoadInterior() {
        crossfade.SetTrigger("levelDone");
        PlayerPrefs.SetString("DateProgress", "Interior");
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Le Cul Puant Interior", true);
        return;
    }

    private void LoadSpaceship() {
        crossfade.SetTrigger("levelDone");
        PlayerPrefs.SetString("DateProgress", "Spaceship");
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Spaceship", true);
        return;
    }
    private void LoadCrashedExterior() {
        crossfade.SetTrigger("levelDone");
        PlayerPrefs.SetString("DateProgress", "End");
        PlayerPrefs.SetString("LCPSpriteState", "Crashed");
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Crashed Exterior", true);
        return;
    }
    private void FinishLevel() {
        crossfade.SetTrigger("levelDone");
        PlayerPrefs.SetString("SisterDialogueState", "PostDate");
        PlayerPrefs.SetString("LCPEntry", "Done");
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Overworld", true);
        return;
    }
}
