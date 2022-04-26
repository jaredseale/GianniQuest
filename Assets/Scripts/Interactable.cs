using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    BoxCollider2D myCollider;
    [SerializeField] GameObject arrow;
    DialogueManager dialogueManager;

    private void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update() {
        ArrowBounce();
        if (dialogueManager.inDialogue) {
            arrow.SetActive(false);
        }
    }

    private void ArrowBounce() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            arrow.SetActive(true);
        } else {
            arrow.SetActive(false);
        }
    }
}
