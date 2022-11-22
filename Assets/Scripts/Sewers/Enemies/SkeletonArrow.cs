using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArrow : MonoBehaviour
{

    Player player;
    public float speed;
    Rigidbody2D myRb;
    [SerializeField] BoxCollider2D myCollider;

    void Start() {
        player = FindObjectOfType<Player>();
        myRb = GetComponent<Rigidbody2D>();

        StartCoroutine(DestroyDelay());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!player.takingDamage) { //only hurt if out of iframes
                player.Hurt();
                FindObjectOfType<PlayerHealth>().HurtPlayer(1);

                Vector2 playerVelo = player.GetComponent<Rigidbody2D>().velocity;
                if (GetComponent<Rigidbody2D>().velocity.x > 0f) { //bounce gianni the other way depending on the direction the arrow is flying
                    player.playerRigidbody.velocity = new Vector2(10f, 12f);
                } else {
                    player.playerRigidbody.velocity = new Vector2(-10f, 12f);
                }
            }

            StartCoroutine(DestroyNextFrame());
        }
    }

    IEnumerator DestroyDelay() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator DestroyNextFrame() {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

}
