using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Sounds like you forgot it was your turn to make Christmas Eve dinner this year, huh?",
        "It's probably too late now to make anything, you could probably go grab a pizza or something really quick.",
        "I heard they're selling them for cheap over at Pizza Hell this week, you should go check that out.",
        "Before you do that, though, I have a favor to ask.",
        "My friend has a massive crush on you and I kinda already set you up on a blind date with her haha oops ;-P",
        "If you meet her at the new French restaurant down the street and she has a nice time, I'll give you a buck (one dollar) to start you out.",
        "Don't mess this up :-)" };
    string[] Init2 = new string[] { "Did you already forget what I said...?",
        "If you go on a date with my friend, I'll give you a dollar for your pizza journey."};
    string[] PostDate = new string[] { "Anousse sent me a text a few minutes ago saying that she had a pretty good time on the date.",
        "So congratulations! And like I said, here's your pizza dollar I promised you :-D"};
    string[] PostDollar = new string[] { "Sorry Gianni, but if you want another dollar, you're gonna have to go on another date, and there are no more in the game.",
        "Huh? What am I talking about?"};
    string[] Night = new string[] { "It's getting pretty late and this belly o' mine is rumblin' and tumblin'.",
        "Are you close to being able to afford a pizza yet?"};

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("SisterDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "PostDate") {
            currentDialogue.dialogue.sentences = PostDate;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "Night") {
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

        if (PlayerPrefs.GetString("SisterDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "PostDate") {
            currentDialogue.dialogue.sentences = PostDate;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "Night") {
            currentDialogue.dialogue.sentences = Night;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("SisterDialogueState") == "Init") {
            PlayerPrefs.SetString("SisterDialogueState", "Init2");
        } else if (PlayerPrefs.GetString("SisterDialogueState") == "PostDate") {
            Dollar myDollar = FindObjectOfType<Dollar>();
            myDollar.CollectDollar();
            PlayerPrefs.SetString("SisterDialogueState", "PostDollar");
        }

    }

}
