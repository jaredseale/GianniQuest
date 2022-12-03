using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentButton : MonoBehaviour
{

    public bool isPressed = false;

    [SerializeField] string buttonPlayerPrefName;
    [SerializeField] GameObject correspondingDoor;
    [SerializeField] Vector3 doorStartPos;
    [SerializeField] Vector3 doorEndPos;
    [SerializeField] float moveSpeed;


    [SerializeField] Sprite pressedButtonSprite;
    [SerializeField] Sprite unpressedButtonSprite;

    BoxCollider2D myCollider;
    Animator myAnim;
    AudioSource myAudio;
    [SerializeField] AudioSource gateAudio;
    SpriteRenderer mySprite;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        mySprite = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt(buttonPlayerPrefName) == 0) {
            isPressed = false;
            mySprite.sprite = unpressedButtonSprite;
            correspondingDoor.gameObject.transform.position = doorStartPos;
        } else {
            isPressed = true;
            mySprite.sprite = pressedButtonSprite;
            correspondingDoor.gameObject.transform.position = doorEndPos;
        }
        
    }


    private void Update() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerFeet")) && !isPressed) {
            isPressed = true;
            PlayerPrefs.SetInt(buttonPlayerPrefName, 1);
            myAudio.Play();
            gateAudio.Play();
            mySprite.sprite = pressedButtonSprite;
            StartCoroutine(MoveDoor());
        }
    }

    IEnumerator MoveDoor() {
        Vector3 startPosition = correspondingDoor.transform.position;
        float time = 0f;

        while (correspondingDoor.transform.position != doorEndPos) {
            correspondingDoor.transform.position = Vector3.Lerp(startPosition, doorEndPos, (time / Vector3.Distance(startPosition, doorEndPos)) * moveSpeed);
            time += Time.deltaTime;
            yield return null;
        }
    }



}
