using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{

    PlayerHealth playerHealth = null;
    AudioSource myAudio = null;
    CircleCollider2D myCollider = null;
    SpriteRenderer mySprite = null;

    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        myAudio = GetComponent<AudioSource>();
        myCollider = GetComponent<CircleCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerHealth.HealPlayer(1);
        }

        myAudio.Play();
        myCollider.enabled = false;
        mySprite.enabled = false;
        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void EnableCollider() { //called by anim
        myCollider.enabled = true;
    }

}
