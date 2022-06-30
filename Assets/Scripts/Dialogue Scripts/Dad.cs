using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] { "Ciao, mio bellissimo bambino.",
        "Your work called. They wanted to remind you that you're scheduled for a shift today, capisce?",
        "I bet they'll pay you a pretty penny or possibly a hundred if you do a good job, hai capito?"};
    string[] Init2 = new string[] { "Oi, hurry down to the restaurant, you're burning daylight, compaesano." };
    string[] FailedSNICO = new string[] { "Cucciolo, don't worry about your mistakes at work. Learn from them and go back to give it your best shot, passerotto." };
    string[] PreDollar = new string[] { "Ascolta patatino, your work check came in the mail. Bravissimo." };
    string[] PostDollar = new string[] { "Mi dispiace, polpetto, you're going to have to wait until your next shift if you want to get paid again." };
    string[] Night = new string[] { "Buona serata, Gianni." };

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("DadDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "FailedSNICO") {
            currentDialogue.dialogue.sentences = FailedSNICO;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "PreDollar") {
            currentDialogue.dialogue.sentences = PreDollar;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Night") {
            currentDialogue.dialogue.sentences = Night;
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
        } else if (PlayerPrefs.GetString("DadDialogueState") == "FailedSNICO") {
            currentDialogue.dialogue.sentences = FailedSNICO;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "PreDollar") {
            currentDialogue.dialogue.sentences = PreDollar;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Night") {
            currentDialogue.dialogue.sentences = Night;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("DadDialogueState") == "Init") {
            PlayerPrefs.SetString("DadDialogueState", "Init2");
        } else if (PlayerPrefs.GetString("DadDialogueState") == "PreDollar") {
            Dollar myDollar = FindObjectOfType<Dollar>();
            myDollar.CollectDollar();
            PlayerPrefs.SetString("DadDialogueState", "PostDollar");
        }

    }

}
