using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHatch : MonoBehaviour
{

    Vector2 flightDirection;
    Vector3 v3flightDirection;
    float flightSpeed = 15;
    float radius = 120f;

    void Start() {
        StartCoroutine(Despawn()); //gets rid of it after a time

        flightDirection = Random.insideUnitCircle.normalized * radius;
        
        if (flightDirection.y < 0) { //ensures it flies to the upper half arc of a circle
            flightDirection = new Vector2(flightDirection.x, -flightDirection.y);
        }

        v3flightDirection = flightDirection;

        if (flightDirection.x < 0f) { //flips the sprite if it's flying to the left
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void Update() {
        transform.position = Vector2.Lerp(transform.position, transform.position + v3flightDirection, Time.deltaTime / flightSpeed);
    }

    IEnumerator Despawn() {

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
