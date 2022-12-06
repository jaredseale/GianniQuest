using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randy : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;
    Animator myAnim;

    string[] Init = new string[] {"Holy moly little guy, you really saved my bacon out there.",
        "I was taking a shortcut through these sewers on my way back from Rick's and I musta made a wrong turn somewhere.",
        "Next thing I know, this arbitrary assortment of goons captures me and tosses me in this little cage.",
        "I guess I owe ya one. Anything to get out of helping my rotten stepson with his Spanish homework.",
        "Yo quiero blow my brains out, know what I'm saying?",
        "Anyways, hows about I follow you around and give you any boosts you need to get around this place?",
        "\"This place\" being the most loosely themed \"sewers\" I've ever floated around in, by the way.",
        "Oh, and here's my map of this area if you want to check that out too. It's not all the way filled out yet, mind you, that's your job bud.",
        "Also, if you haven't already, take a look at the controls on your pause screen for reminders on how to uhhh... use me."};

    private void Start() {

        if (PlayerPrefs.GetInt("HasDoubleJump") == 1) {
            Destroy(gameObject);
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
        myAnim = GetComponent<Animator>();
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
            GetComponent<NPC>().enabled = false;
            GetComponent<Interactable>().enabled = false;
            myAnim.SetTrigger("fadeout");
            PlayerPrefs.SetInt("HasDoubleJump", 1);
        }
    }

    private void SetDialogue() {
        currentDialogue.dialogue.sentences = Init;
    }

    public void DestroyRandy() { //called by anim
        Destroy(gameObject);
    }
}