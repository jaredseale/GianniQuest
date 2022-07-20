using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lairry : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Yo have you ever heard of Smirsauce? It's this gnarly new cocktail I just invented like several seconds ago.",
        "Basically pretty much what you do is mix together some soy sauce and vodka and slurp it down. Everyone's talking about it!",
        "Hey, you look like someone who might be down for a little challenge.",
        "Tell you what, if you can throw back more Smirsauces than me in a minute, I'll give you a dollar.",
        "Talk to me again if you think you're up for it!"};
    string[] BeforeContest = new string[] { "Alright! Let's do this!" };
    string[] LostContest = new string[] { "Not bad HRRRNNNGGGHHHHH but I think I beat ya this time OUUGGHHH.",
        "That was fun though *hic* BRUUUUUUUUPPP, we should do that again later or like AWOOOOUUUGGGHH right now? Again right now?"};
    string[] WonContest = new string[] { "Whoooa dude you totally smo- ERRRGGGGHHHH -ked me, nice job UNNNGGGHH.",
        "Here's your dollar man, you *hic* earned OOOOOOUUUUGGGHHH it!"};
    string[] PostDollar = new string[] { "I don't think I have much longer to live." };

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();
        SetDialogue();
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }
        }

        SetDialogue();

        void PlayAction() {

            if (PlayerPrefs.GetString("LairryDialogueState") == "Init") {
                PlayerPrefs.SetString("LairryDialogueState", "BeforeContest");
            } else if (PlayerPrefs.GetString("LairryDialogueState") == "BeforeContest") {
                //load game scene
            } else if (PlayerPrefs.GetString("LairryDialogueState") == "LostContest") {
                PlayerPrefs.SetString("LairryDialogueState", "BeforeContest");
            } else if (PlayerPrefs.GetString("LairryDialogueState") == "WonContest") {
                Dollar myDollar = FindObjectOfType<Dollar>();
                myDollar.CollectDollar();
                PlayerPrefs.SetString("LairryDialogueState", "PostDollar");
            }

        }
    }

    private void SetDialogue() {
        if (PlayerPrefs.GetString("LairryDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("LairryDialogueState") == "BeforeContest") {
            currentDialogue.dialogue.sentences = BeforeContest;
        } else if (PlayerPrefs.GetString("LairryDialogueState") == "LostContest") {
            currentDialogue.dialogue.sentences = LostContest;
        } else if (PlayerPrefs.GetString("LairryDialogueState") == "WonContest") {
            currentDialogue.dialogue.sentences = WonContest;
        } else if (PlayerPrefs.GetString("LairryDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        }
    }
}
