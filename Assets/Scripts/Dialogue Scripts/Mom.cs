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

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetString("MomDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "Init2") {
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

        if (PlayerPrefs.GetString("MomDialogueState") == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (PlayerPrefs.GetString("MomDialogueState") == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetString("MomDialogueState") == "Init") {
            PlayerPrefs.SetString("MomDialogueState", "Init2");
        }

    }

}
