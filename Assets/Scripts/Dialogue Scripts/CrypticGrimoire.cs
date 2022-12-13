using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrypticGrimoire : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Laying on the ground in front of a sewer grate is a cryptic grimoire of some sort.",
        "Recalling your studies of arcane literature, you translate the runes of a random page in the middle of the book.",
        "\"Press Q or Z on your keyboard to conjure a clone of yourself. You may only have one clone active at a time.\"",
        "\"You can refer to the controls in the pause menu for a reminder of these instructions.\"",
        "You commit the mystical incantation to memory and leave the grimoire back on the ground."};

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
            PlayerPrefs.SetInt("HasCloner", 1);
        }
    }

    private void SetDialogue() {
        currentDialogue.dialogue.sentences = Init;
    }
}