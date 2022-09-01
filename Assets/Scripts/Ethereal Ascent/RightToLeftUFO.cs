using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightToLeftUFO : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        int colorPicker = Random.Range(0, 4);

        if (colorPicker == 0) {
            mySpriteRenderer.color = new Color(0f, 1f, 1f);
        } else if (colorPicker == 1) {
            mySpriteRenderer.color = new Color(1f, 0f, 1f);
        } else if (colorPicker == 2) {
            mySpriteRenderer.color = new Color(1f, 1f, 0f);
        } else if (colorPicker == 3) {
            mySpriteRenderer.color = new Color(1f, 1f, 1f);
        }
    }

    void Update() {
        myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
        if (gameObject.transform.position.x < -80f) {
            Destroy(gameObject);
        }
    }
}
