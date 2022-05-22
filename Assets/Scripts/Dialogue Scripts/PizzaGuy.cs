using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaGuy : MonoBehaviour
{

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;

    string[] Init = new string[] {"Hey guy, welcome to Pizza Hell, where the lakes of fire are hot and the pizzas are even hotter.",
        "If you want one of our fresh 'zas, it's going to run you around six dollars. Actually, it is going to run you exactly six dollars. To the cent.",
        "It looks like you don't quite have enough dough on you. Come back when you can afford the dough. Do you get the joke there." };
    string[] HasMoney = new string[] {"Hey little man, welcome back to Pizza Hell. You look like you have just enough money for a premium pie.",
        "Here you go, one no cheese pepperoni spinach bacon ranch pineapple crab meat cucumber stuffed crust deep dish deep fried honey baked wood fired pizza." };

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
