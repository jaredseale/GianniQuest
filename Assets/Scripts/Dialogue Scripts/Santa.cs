using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Ho ho ho, little Gianni, what a Christmas disaster!",
        "Here I am, soaring through the air in this winter wonderland, when one of my reindeer gets sick and careens us into a tree!",
        "My magical bag got caught on a limb and dear oh dear, I lost a few of my gifts for all you good boys and girls out there!",
        "I've been trying to gather them myself, but these old bones o' mine aren't what they used to be, believe you me.",
        "Ho ho ho, here's an idea! I don't suppose you would mind lending me a hand while I tend to my reindeer?",
        "I've found a few already, but I seem to still be missing around twenty gifts.",
        "They're probably spread all over this area, so keep an eye out for them!",
        "Come to think of it, I see one up there and to the right. See if you can grab that!"};
    string[] Init2 = new string[] { "I'm still missing a few of the gifts I dropped. Have a look around and see if you can spot any." };
    string[] PreDollar = new string[] { "Ho ho ho, thank you so much, Gianni! That looks like everything that fell out of my bag.",
        "Although on second thought, this rusty key doesn't seem to belong to me. I'll let you keep that one.",
        "And since you've been an extra good boy this year, why don't I reward you with a crisp United States one dollar bill that my elves made? Don't tell your government!"};
    string[] PostDollar = new string[] { "My reindeer looks to be in ship-shape now. I blame that hot dog they ate earlier.",
        "I suppose it's time for me to skedaddle. Happy holidays, ho ho ho!"};

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
                FindObjectOfType<EAManager>().ItemCounterSlideIn();
            } else if (PlayerPrefs.GetString("SantaDialogueState") == "PreDollar") {
                PlayerPrefs.SetString("SantaDialogueState", "PostDollar");
                PlayerPrefs.SetString("SewersEntry", "Open");
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