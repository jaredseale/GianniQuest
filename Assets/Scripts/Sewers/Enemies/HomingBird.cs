using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBird : MonoBehaviour
{

    public float attackNumber;
    Player player;
    public float speed;
    float angleOfIncline;
    Rigidbody2D myRB;
    CircleCollider2D myCollider;
    AudioSource myAudio;

    void Start() {
        player = FindObjectOfType<Player>();
        StartCoroutine(WaitToAttack());
        myRB = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        myAudio = GetComponent<AudioSource>();
        myAudio.pitch = Random.Range(0.8f, 1.2f);
        FlipSprite();
    }

    private void Update() {
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
    }

    IEnumerator WaitToAttack() {
        yield return new WaitForSeconds(1f + attackNumber);

        FlipSprite();
        myAudio.Play();

        angleOfIncline = Mathf.Atan2(player.gameObject.transform.position.y - gameObject.transform.position.y,
            player.gameObject.transform.position.x - gameObject.transform.position.x);
        myRB.velocity = new Vector2(Mathf.Cos(angleOfIncline) * speed, Mathf.Sin(angleOfIncline) * speed);
    }

    void FlipSprite() {
        if (gameObject.transform.position.x > player.gameObject.transform.position.x) {
            gameObject.transform.localScale = new Vector2(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        } else {
            gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        }
    }

}
