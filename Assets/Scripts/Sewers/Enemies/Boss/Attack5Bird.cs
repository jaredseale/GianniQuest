using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack5Bird : MonoBehaviour
{

    public string attackDirection;
    public int speed;
    public Vector2 homePos;

    Rigidbody2D myRB;
    CircleCollider2D myCollider;
    Player player;
    AudioSource myAudio;

    void Start() {
        player = FindObjectOfType<Player>();
        myRB = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        myAudio = GetComponent<AudioSource>();
        myAudio.pitch = Random.Range(0.7f, 1.2f);

        if (attackDirection == "right") {
            myRB.velocity = new Vector2(speed, 0f);
        } else {
            myRB.velocity = new Vector2(-speed, 0f);
        }
    }

    void Update() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {

            if (!player.takingDamage) { //only hurt if out of iframes
                player.Hurt();
                FindObjectOfType<PlayerHealth>().HurtPlayer(1);
            }

            Vector2 playerVelo = player.GetComponent<Rigidbody2D>().velocity;
            if (player.transform.position.x > gameObject.transform.position.x) { //bounce gianni the other way depending on where he is to the enemy
                player.playerRigidbody.velocity = new Vector2(10f, 12f);
            } else {
                player.playerRigidbody.velocity = new Vector2(-10f, 12f);
            }
        }

        if (gameObject.transform.position.x < -42f || gameObject.transform.position.x > -3f) {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        gameObject.transform.position = homePos;
        Start();
    }

}
