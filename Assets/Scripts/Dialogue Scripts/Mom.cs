using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] { "Good morning my sweet little meatball!",
        "Your math teacher, Mrs. Crimt'n, called and said you needed to swing by the school, apparently you have some overdue homework.",
        "If you manage to get it done, I'll give you a beautiful dollared bill as a reward."};
    string[] Init2 = new string[] { "Go on down to the school, my precious little rigatoni rascal!" };
    string[] PreDollar = new string[] { "Welcome home! I heard from your teacher that you aced your homework!",
        "Just like I promised, here is your singular dollar William (bill for short)."};
    string[] PostDollar = new string[] { "Unfortunately, that was my last money, and I need to get back to tending the house bar.",
        "I'm sure you can use your fully developed Gianni brain to find some other dollars!"};
    string[] Night = new string[] { "Are you having such a good time today, Gianni?",
        "Don't forget, it's your turn to get dinner tonight, so don't dilly dally too long on that!"};

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("MomDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "PreDollar") {
            currentDialogue.dialogue.sentences = PreDollar;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "Night") {
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

        if (PlayerPrefs.GetString("MomDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "PreDollar") {
            currentDialogue.dialogue.sentences = PreDollar;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "PostDollar") {
            currentDialogue.dialogue.sentences = PostDollar;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "Night") {
            currentDialogue.dialogue.sentences = Night;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("MomDialogueState") == "Init") {
            PlayerPrefs.SetString("MomDialogueState", "Init2");
        } else if (PlayerPrefs.GetString("MomDialogueState") == "PreDollar") {
            Dollar myDollar = FindObjectOfType<Dollar>();
            myDollar.CollectDollar();
            PlayerPrefs.SetString("MomDialogueState", "PostDollar");
            PlayerPrefs.SetString("SchoolEntry", "Done");
        }   

    }

}
