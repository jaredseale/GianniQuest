using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableButton : MonoBehaviour
{

    public bool isTimed;
    public bool isPressed = false;

    public GameObject correspondingDoor;

    [SerializeField] Sprite pressedButtonSprite;
    [SerializeField] Sprite unpressedButtonSprite;

    BoxCollider2D myCollider;
    Animator myAnim;
    AudioSource myAudio;
    SpriteRenderer mySprite;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        isPressed = true;
        myAudio.Play();
        mySprite.sprite = pressedButtonSprite;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isPressed = false;
        mySprite.sprite = unpressedButtonSprite;
    }

}
