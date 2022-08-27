using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody2D myRigidbody;
    [SerializeField] Sprite[] cloudSpriteArray;
    SpriteRenderer mySpriteRenderer;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = cloudSpriteArray[Random.Range(0, 19)];

        moveSpeed = Random.Range(moveSpeed, moveSpeed + 1.5f); //move speed determines the below props
        gameObject.transform.localScale = new Vector3(moveSpeed, moveSpeed, moveSpeed);
        mySpriteRenderer.sortingOrder = (int)(533f * moveSpeed - 1166f); //linear equation to sort clouds based on how fast they're going

        float newColorValue = Mathf.Clamp((0.25f * moveSpeed + 0.625f), .75f, 1f); //linear equation to darken clouds based on how far away they appear
        mySpriteRenderer.color = new Color(newColorValue, newColorValue, newColorValue);

    }

    void Update() {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        if (gameObject.transform.position.x > 80f) {
            Destroy(gameObject);
        }
    }
}
