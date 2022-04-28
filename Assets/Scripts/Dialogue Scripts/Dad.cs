using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] { "Ciao, mio bellissimo bambino.",
        "Your work called in. They ehhhh wantin' you to work a shift today, capisce?",
        "I bet they'll pay you a pretty penny or possibly a hundred if you do a good job, hai capito?"};
    string[] Init2 = new string[] { "Oi, fretta down to the restaurant, you're burning daylight, compaesano." };

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("DadDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        }
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }
        }

        if (PlayerPrefs.GetString("DadDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("DadDialogueState") == "Init") {
            PlayerPrefs.SetString("DadDialogueState", "Init2");
        }

    }

}
