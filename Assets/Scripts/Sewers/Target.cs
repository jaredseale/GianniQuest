using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public bool isActivated = false;
    public bool isRightFacing;

    [SerializeField] string targetPlayerPrefName;
    [SerializeField] GameObject correspondingDoor;
    [SerializeField] Vector3 doorStartPos;
    [SerializeField] Vector3 doorEndPos;
    [SerializeField] float moveSpeed;


    [SerializeField] Sprite activatedTargetSprite;
    [SerializeField] Sprite unactivatedButtonSprite;

    BoxCollider2D myCollider;
    AudioSource myAudio;
    [SerializeField] AudioSource gateAudio;
    SpriteRenderer mySprite;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        myAudio = GetComponent<AudioSource>();
        mySprite = GetComponent<SpriteRenderer>();

        if (targetPlayerPrefName != "") {
            if (PlayerPrefs.GetInt(targetPlayerPrefName) == 0) {
                isActivated = false;
                mySprite.sprite = unactivatedButtonSprite;
                correspondingDoor.gameObject.transform.position = doorStartPos;
            } else {
                isActivated = true;
                mySprite.sprite = activatedTargetSprite;
                correspondingDoor.gameObject.transform.position = doorEndPos;
            } 
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet") && !isActivated) {

            if (isRightFacing && (other.GetComponent<Rigidbody2D>().velocity.x > 0f)) { //checks if the bullet is coming from the correct direction
                return;
            }

            if (!isRightFacing && (other.GetComponent<Rigidbody2D>().velocity.x < 0f)) {
                return;
            }

            Destroy(other.gameObject);
            isActivated = true;

            if (targetPlayerPrefName != "") {
                PlayerPrefs.SetInt(targetPlayerPrefName, 1);
            }
            
            myAudio.Play();
            gateAudio.Play();
            mySprite.sprite = activatedTargetSprite;
            StartCoroutine(MoveDoor());
        }
    }
}
