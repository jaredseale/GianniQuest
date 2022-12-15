using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBag : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Score! Looks like some poor sap accidentally flushed away their bag of cherry bombs.",
        "You stuff as many cherry bombs into your pockets as your jorted denim can muster.",
        "Drop a cherry bomb on the ground using the E or C key. Careful though, these things pack a punch, and can hurt you too!",
        "As always, you can get a reminder of these controls in the pause menu." };

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
            PlayerPrefs.SetInt("HasBomb", 1);
        }
    }

    private void SetDialogue() {
        currentDialogue.dialogue.sentences = Init;
    }
}