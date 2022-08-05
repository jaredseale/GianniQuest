using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;
    [SerializeField] GameObject englishTestOnDesk;
    [SerializeField] GameObject mathTestOnDesk;

    string[] Init = new string[] { "Gianni, I am so excited to be here at school on this wonderful Christmas Eve!",
        "It seems that you're late on a couple of your assignments this week.",
        "If you can briskly fill at least one of the exams I've placed on the desks, we can both go home!"};
    string[] Init2 = new string[] { "Go have a look at one of the exams on the desks and talk to me again after you've filled one out." };
    string[] MathTestWrong = new string[] { "Hmmm... it doesn't look like your answer for the math exam is quite right. Go have another try." };
    string[] MathTestDone = new string[] { "Wowee! Wow! Yes! Yes!!! You're done! Congratulations!!! Wow! Woo hoo! Yes! I can leave now! I mean you can leave now!",
        "(Reminder, you can press TAB to pause and exit to the overworld from there.)",
        "(This is me, Jared, telling you this, not the teacher. Diagesis!)"};
    string[] EnglishTestWrong = new string[] { "Hmmm... it looks like you may have left a word or two blank on your English exam. Go see if you can fix that." };
    string[] EnglishTestDone = new string[] { "Those are certainly some interesting word choices. Now, write a story using the words you came up with!" };
    string[] PostEssay = new string[] { "Gianni that was simply one of the worst pieces of literature I've ever read.",
        "However, you did complete the assignment, so I am legally obligated to congratulate you and send you on your way! Now begone!",
        "(Reminder, you can press TAB to pause and exit to the overworld from there.)",
        "(This is me, Jared, telling you this, not the teacher. Diagesis!)"};
    string[] LevelComplete = new string[] { "You can leave now Gianni! Thank you!" ,
        "(Reminder, you can press TAB to pause and exit to the overworld from there.)",};

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
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "MathTestWrong") {
            currentDialogue.dialogue.sentences = MathTestWrong;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "MathTestDone") {
            currentDialogue.dialogue.sentences = MathTestDone;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "EnglishTestWrong") {
            currentDialogue.dialogue.sentences = EnglishTestWrong;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "EnglishTestDone") {
            currentDialogue.dialogue.sentences = EnglishTestDone;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "PostEssay") {
            currentDialogue.dialogue.sentences = PostEssay;
        } else if (PlayerPrefs.GetString("TeacherDialogueState") == "LevelComplete") {
            currentDialogue.dialogue.sentences = LevelComplete;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("TeacherDialogueState") == "Init") {
            PlayerPrefs.SetString("TeacherDialogueState", "Init2");
            mathTestOnDesk.GetComponent<Interactable>().enabled = true;
            englishTestOnDesk.GetComponent<Interactable>().enabled = true;
        }

        if (PlayerPrefs.GetString("TeacherDialogueState") == "MathTestDone") {
            mathTestOnDesk.GetComponent<Interactable>().enabled = false;
            englishTestOnDesk.GetComponent<Interactable>().enabled = false;
            PlayerPrefs.SetString("MomDialogueState", "PreDollar");
            PlayerPrefs.SetString("TeacherDialogueState", "LevelComplete");
        }

        if (PlayerPrefs.GetString("TeacherDialogueState") == "EnglishTestDone") {
            mathTestOnDesk.GetComponent<Interactable>().enabled = false;
            englishTestOnDesk.GetComponent<Interactable>().enabled = false;
            PlayerPrefs.SetString("TeacherDialogueState", "PostEssay");
            //play madlibs screen
        }

        if (PlayerPrefs.GetString("TeacherDialogueState") == "PostEssay") {
            PlayerPrefs.SetString("MomDialogueState", "PreDollar");
            PlayerPrefs.SetString("TeacherDialogueState", "LevelComplete");
        }
    }

}
