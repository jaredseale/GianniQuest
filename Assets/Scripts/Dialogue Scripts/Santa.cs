using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Ho ho ho, my dear Gianni, what a Christmas disaster!",
        "Here I am, flying through the air in this winter wonderland you call Arizona, when one of my reindeer gets sick and careens us into a telephone pole.",
        "My magical bag got caught on a transformer and ripped, and I lost some of my gifts for all you good boys and girls out there.",
        "I don't suppose you would mind lending me a hand while I tend to my reindeer?",
        "I took inventory a short while ago, and I believe I'm missing twenty presents."};
    string[] Init2 = new string[] { "I'm still missing a few of the presents I dropped. Have a look around and see if you can spot any." };
    string[] PreDollar = new string[] { "Ho ho ho, thank you so much, Gianni! That looks like everything that fell out of my bag.",
        "Although on second thought, this rusty key doesn't seem to belong to me. I'll let you keep that one.",
        "And since you've been an extra good boy this year, why don't I reward you with a crisp United States one dollar bill that my elves made? Don't tell your government!"};
    string[] PostDollar = new string[] { "My reindeer look to be in ship-shape now. I blame that hot dog they ate earlier." };

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

            if (PlayerPrefs.GetString("SantaDialogueState") == "Init") {
                PlayerPrefs.SetString("SantaDialogueState", "Init2");
            } else if (PlayerPrefs.GetString("SantaDialogueState") == "PreDollar") {
                PlayerPrefs.SetString("SantaDialogueState", "PostDollar");
                PlayerPrefs.SetInt("EADataManagement", 0);
                Dollar myDollar = FindObjectOfType<Dollar>();
                myDollar.CollectDollar();
            }
        }
    }

    private void SetDialogue() {
        if (PlayerPrefs.GetString("SantaDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("SantaDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("SantaDialogueState") == "PreDollar") {
            currentDialogue.dialogue.sentences = PreDollar;
        } else if (PlayerPrefs.GetString("SantaDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        }
    }
}