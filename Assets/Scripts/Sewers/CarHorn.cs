using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHorn : MonoBehaviour
{

    Player player;
    AudioSource myAudio;

    void Start() {
        player = FindObjectOfType<Player>();
        myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PlayerFeet") && (player.GetComponent<Rigidbody2D>().velocity.y < 0f)){
            myAudio.Play();
        }
    }
}
