using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicksKey : MonoBehaviour
{
    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;
    [SerializeField] AudioClip keyJingle;

    string[] Init = new string[] { "You pick up the glowing key you find sitting on the shelf. It fits nicely into your jorts pocket." };

    private void Start() {
        if (PlayerPrefs.GetString("RicksKey") != "Uncollected") {
            Destroy(gameObject);
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
        currentDialogue.dialogue.sentences = Init;
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }
        }

        void PlayAction() {
            PlayerPrefs.SetString("RicksKey", "Collected");
            dialogueManager.GetComponent<AudioSource>().PlayOneShot(keyJingle);
            Destroy(gameObject);
        }
    }

}
