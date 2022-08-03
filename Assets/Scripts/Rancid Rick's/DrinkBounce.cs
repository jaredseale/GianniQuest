using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkBounce : MonoBehaviour
{
    public int moveSpeed;
    Rigidbody2D myRigidbody;
    float speedModifier = 2.5f;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }



    public void SetMoveSpeed(int newSpeed) {
        float xSpeed = Random.Range((-newSpeed + 1) / speedModifier, (newSpeed - 1) / speedModifier);
        float ySpeed = Random.Range((-newSpeed + 1) / speedModifier, (newSpeed - 1) / speedModifier);

        myRigidbody.velocity = new Vector2(xSpeed, ySpeed);
    }

}
