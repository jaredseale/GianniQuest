using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"If you don't know what to do for dinner, I guess you could go get some pizza or something.",
        "They're selling them for six bucks (dollars) over at Pizza Hell this week.",
        "Before you do that, though, I have a favor to ask.",
        "My friend has a massive crush on you and I kinda already set you up on a blind date with her haha oops ;-P",
        "If you meet her at the new French restaurant down the street and she has a nice time, I'll give you a buck (one dollar) to start you out.",
        "Don't mess this up :-)" };
    string[] Init2 = new string[] { "Did you already forget what I said...?",
        "If you go on a date with my friend, I'll give you a dollar for your pizza journey."};

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("SisterDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        }
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }

            if (PlayerPrefs.GetString("SisterDialogueState") == "Init") {
                currentDialogue.dialogue.sentences = Init;
            } else if (PlayerPrefs.GetString("SisterDialogueState") == "Init2") {
                currentDialogue.dialogue.sentences = Init2;
            }
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("SisterDialogueState") == "Init") {
            PlayerPrefs.SetString("SisterDialogueState", "Init2");
        }

    }

}
