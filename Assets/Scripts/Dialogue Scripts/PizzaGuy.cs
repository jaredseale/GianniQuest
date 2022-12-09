using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaGuy : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Happy holidays and welcome to Pizza Hell, home of the sinfully delicious, brimstone-fired Infernal Deep Dish.",
        "If you want one of our fresh 'zas, it's going to run you around six dollars. Actually, it is going to run you exactly six dollars. To the cent.",
        "It looks like you don't quite have enough dough on you. Come back when you can afford the dough. Do you get the joke there.",
        "Also, in case you forgot, you can press TAB to leave. Our door is only one-way thanks to budget cuts."};
    string[] HasMoney = new string[] {"Hey big guy, welcome back to Pizza Hell. You look like you have just enough money for a premium slice, or eight.",
        "One purgaterrific pie, coming right up!" };

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (PlayerPrefs.GetInt("Dollars") < 6) {
            currentDialogue.dialogue.sentences = Init;
        } else {
            currentDialogue.dialogue.sentences = HasMoney;
        }
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }
        }

        if (PlayerPrefs.GetInt("Dollars") < 6) {
            currentDialogue.dialogue.sentences = Init;
        } else {
            currentDialogue.dialogue.sentences = HasMoney;
        }
    }

    void PlayAction() {

        if (PlayerPrefs.GetInt("Dollars") >= 6) {
            //trigger end of game
        }
    }
}
