using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : MonoBehaviour
{


    [SerializeField] string playerPrefName;
    PlayerHealth playerHealth = null;
    AudioSource myAudio = null;
    CircleCollider2D myCollider = null;
    SpriteRenderer mySprite = null;

    void Start() {

        if (PlayerPrefs.GetInt(playerPrefName) == 1) {
            Destroy(gameObject);
        }

        playerHealth = FindObjectOfType<PlayerHealth>();
        myAudio = GetComponent<AudioSource>();
        myCollider = GetComponent<CircleCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerHealth.IncreaseMaxHealth();
            myAudio.Play();
            myCollider.enabled = false;
            mySprite.enabled = false;
            PlayerPrefs.SetInt(playerPrefName, 1);
            StartCoroutine(DestroyDelay());
        }
    }

    IEnumerator DestroyDelay() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void EnableCollider() { //called by anim
        myCollider.enabled = true;
    }
}
