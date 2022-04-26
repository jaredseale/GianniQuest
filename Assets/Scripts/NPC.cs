using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    BoxCollider2D myCollider;
    Player playerController;
    [SerializeField] DialogueTrigger dialogueTrigger;
    [SerializeField] float thisSpeakerPitch = 1f;
    public DialogueManager dialogueManager;

    private void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        playerController = FindObjectOfType<Player>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update() {
        Talk();
    }
    private void Talk() {
        if (Input.GetButtonDown("Up") //if 1) pressed up and 2) on top of loading zone and 3) game not paused and 4) not already in dialogue
            && myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))
            && GameObject.Find("Game Manager").GetComponent<Pause>().gamePaused == false
            && dialogueManager.inDialogue == false) {
            dialogueManager.dialogueTarget = this.gameObject;
            playerController.canMove = false;
            FindObjectOfType<DialogueManager>().speakerPitch = thisSpeakerPitch;
            dialogueTrigger.TriggerDialogue();
        }
    }
}
