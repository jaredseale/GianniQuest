using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;
    [SerializeField] GameObject testOnDesk;

    string[] Init = new string[] { "Gianni, I am so excited to be here on this Beautiful Saturday Afternoon!",
        "It seems that you're late on one of your assignments this week.",
        "If you can briskly fill our your worksheet I've placed on your desk, we can both go home!"};
    string[] Init2 = new string[] { "Go have a look at the worksheet on your desk and then talk to me once you've finished it." };
    string[] TestWrong = new string[] { "Hmmm... it doesn't look like your answer is quite right. Go have another look." };
    string[] TestDone = new string[] { "Wowee! Wow! Yes! Yes!!! You're done! Congratulations!!! Wow! Woo hoo! Yes! You can leave now!",
        "(Press tab to pause and you can exit to the overworld from there.)",
        "(This is me, Jared, telling you this, not the teacher. Diagesis!)"};

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else {
            currentDialogue.dialogue.sentences = Init2;
        }
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }
        }

        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "TestWrong") {
            currentDialogue.dialogue.sentences = TestWrong;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "TestDone") {
            currentDialogue.dialogue.sentences = TestDone;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init") {
            PlayerPrefs.SetString("TeacherDialogueState", "Init2");
            testOnDesk.GetComponent<Interactable>().enabled = true;
        }

        if (PlayerPrefs.GetString("TeacherDialogueState") == "TestDone") {
            testOnDesk.GetComponent<Interactable>().enabled = false;
            PlayerPrefs.SetString("MomDialogueState", "PreDollar");
        }

    }

}
