using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Player player;
    public float speed;
    Rigidbody2D myRb;
    CircleCollider2D myCollider;

    void Start() {
        player = FindObjectOfType<Player>();
        myRb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();

        if (player.facingRight) { //determining which way to send the bullet
            myRb.velocity = new Vector2(speed, 0f);
        } else {
            myRb.velocity = new Vector2(-speed, 0f);
        }

        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(DestroyNextFrame());
        }
    }

    IEnumerator DestroyNextFrame() {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

}
