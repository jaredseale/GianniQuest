using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableButton : MonoBehaviour
{

    public bool isTimed;
    public bool isPressed = false;

    [SerializeField] GameObject correspondingDoor;
    [SerializeField] Vector3 doorStartPos;
    [SerializeField] Vector3 doorEndPos;
    [SerializeField] float openSpeed;
    [SerializeField] float closeSpeed;


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

        isPressed = false;
    }


    private void Update() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerFeet"))) {
            if (isPressed == false) {
                isPressed = true;
                myAudio.Play();
                mySprite.sprite = pressedButtonSprite;
                StopAllCoroutines();
                StartCoroutine(OpenDoor());
            }
        } else {
            if (isPressed == true) {
                isPressed = false;
                mySprite.sprite = unpressedButtonSprite;
                StopAllCoroutines();
                StartCoroutine(CloseDoor());
            }
        }
    }

    IEnumerator OpenDoor() {
        Vector3 startPosition = correspondingDoor.transform.position;
        float time = 0f;

        while (correspondingDoor.transform.position != doorEndPos) {
            correspondingDoor.transform.position = Vector3.Lerp(startPosition, doorEndPos, (time / Vector3.Distance(startPosition, doorEndPos)) * openSpeed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CloseDoor() {
        Vector3 startPosition = correspondingDoor.transform.position;
        float time = 0f;

        while (correspondingDoor.transform.position != doorStartPos) {
            correspondingDoor.transform.position = Vector3.Lerp(startPosition, doorStartPos, (time / Vector3.Distance(startPosition, doorStartPos)) * closeSpeed);
            time += Time.deltaTime;
            yield return null;
        }
    }


}
