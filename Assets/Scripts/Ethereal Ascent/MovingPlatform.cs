using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public GameObject platformPathStart;
    public GameObject platformPathEnd;
    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public Player myPlayer;
    BoxCollider2D myCollider;

    void Start() {
        startPosition = platformPathStart.transform.position;
        endPosition = platformPathEnd.transform.position;
        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        myPlayer = FindObjectOfType<Player>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (transform.position == endPosition) {
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }
        if (transform.position == startPosition) {
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }

        /*if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerFeet")) && myPlayer.playerRigidbody.velocity.y <= 0f) {
            Debug.Log("it's happenin");
            myPlayer.transform.SetParent(gameObject.transform, true);
        } else {
            myPlayer.transform.parent = null;
        }*/

    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed) {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (obj.transform.position != target) {
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0f) {
            col.gameObject.transform.SetParent(gameObject.transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        col.gameObject.transform.parent = null;
    }
}
