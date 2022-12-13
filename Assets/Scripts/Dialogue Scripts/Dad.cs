using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] { "Buona vigilia di Natale, mio bellissimo bambino.",
        "Your work called. They wanted to remind you that you're scheduled for a shift today, capisce?",
        "I bet they'll pay you a pretty penny or possibly a hundred if you do a good job, hai capito?"};
    string[] Init2 = new string[] { "Oi, hurry down to the restaurant, you're burning daylight, compaesano." };
    string[] FailedSNICO = new string[] { "Cucciolo, don't worry about your mistakes at work. Learn from them and go back to give it your best shot, passerotto." };
    string[] PreDollar = new string[] { "Ascolta patatino, your work check came in the mail. Bravissimo." };
    string[] PostDollar = new string[] { "Mi dispiace, polpetto, you're going to have to wait until your next shift if you want to get paid again." };
    string[] Night = new string[] { "Buona serata, Gianni." };
    string[] Sewers = new string[] { "Dio mio, Gianni, what's causing all that racket around the house?",
        "If you're getting up to any trouble, I guess I can't stop you, but I you gotta be safe around these parts.",
        "Here, take my Tom Gun. It will keep you sano e salvo if you happen to encounter anything pericoloso.",
        "When you're not near any buildings, right click to fire the Tom Gun. It's as easy as that, signorino."};
    string[] Sewers2 = new string[] { "Remember, as long as you're not around any buildings, you can use the Tom Gun by right clicking."};


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
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Sewers") {
            currentDialogue.dialogue.sentences = Sewers;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Sewers2") {
            currentDialogue.dialogue.sentences = Sewers2;
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
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Sewers") {
            currentDialogue.dialogue.sentences = Sewers;
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Sewers2") {
            currentDialogue.dialogue.sentences = Sewers2;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("DadDialogueState") == "Init") {
            PlayerPrefs.SetString("DadDialogueState", "Init2");
        } else if (PlayerPrefs.GetString("DadDialogueState") == "PreDollar") {
            Dollar myDollar = FindObjectOfType<Dollar>();
            myDollar.CollectDollar();
            PlayerPrefs.SetString("DadDialogueState", "PostDollar");
            PlayerPrefs.SetInt("SNICODataManagement", 0);
        } else if (PlayerPrefs.GetString("DadDialogueState") == "Sewers") {
            PlayerPrefs.SetInt("HasGun", 1);
            PlayerPrefs.SetString("DadDialogueState", "Sewers2");
        }

    }

}
