using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] { "Gianni, I am so excited to be here on this Beautiful Saturday Afternoon!",
        "It seems that you're late on one of your assignments this week.",
        "If you can briskly fill our your worksheet I've placed on your desk, we can both go home!"};
    string[] Init2 = new string[] { "Go have a look at the worksheet on your desk and bring it back to me once you've finished it." };

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        }
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }

            if (PlayerPrefs.GetString("TeacherDialogueState") == "Init") {
                currentDialogue.dialogue.sentences = Init;
            } else if (PlayerPrefs.GetString("TeacherDialogueState") == "Init2") {
                currentDialogue.dialogue.sentences = Init2;
            }
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init") {
            PlayerPrefs.SetString("TeacherDialogueState", "Init2");
        }

    }

}
