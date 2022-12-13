using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brother : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] { "...thanks midnas_armpit44 for the gift sub to ecchiganondorf... oh hey Gianni.",
        "Everyone in the house said they needed to talk to you.",
        "...uh..."};
    string[] Init2 = new string[] { "If I were you, which I'm not by the way, I'd go and see what everyone in the house has to say." };
    string[] Night = new string[] { "Hey Gianni. Uhhhh, can you give me a minute, I'm on an insane pace right now." };
    string[] Sewers = new string[] { "Oh, hey Gianni. There was kind of a loud noise in the house just a second ago, you know anything about that?" };

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("BrotherDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("BrotherDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("BrotherDialogueState") == "Night") {
            currentDialogue.dialogue.sentences = Night;
        } else if (PlayerPrefs.GetString("BrotherDialogueState") == "Sewers") {
            currentDialogue.dialogue.sentences = Sewers;
        }
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }
        }

        if (PlayerPrefs.GetString("BrotherDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("BrotherDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("BrotherDialogueState") == "Night") {
            currentDialogue.dialogue.sentences = Night;
        } else if (PlayerPrefs.GetString("BrotherDialogueState") == "Sewers") {
            currentDialogue.dialogue.sentences = Sewers;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("BrotherDialogueState") == "Init") {
            PlayerPrefs.SetString("BrotherDialogueState", "Init2");
        }

    }

}
