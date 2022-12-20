using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField] GameObject homingBirds;
    [SerializeField] GameObject bossSprite;

    int attackTurnNumber;
    int attackID;
    int lastAttackID;

    CircleCollider2D myCollider;
    Player player;
    Animator myAnim;

    void Start() {
        myCollider = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player>();
        myAnim = GetComponent<Animator>();
        attackTurnNumber = 1;
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

    public void RollNextAttack() {
        if (attackTurnNumber % 5 == 0) {
            attackID = 1;
            lastAttackID = 1;
        } else {
            while (attackID != lastAttackID) {
                attackID = Random.Range(2, 6);
            }

            lastAttackID = attackID;
            myAnim.SetInteger("Attack", attackID);
        }
    }

    public void SpawnHomingBirds() {
        Instantiate(homingBirds, new Vector2(0f, 0f), Quaternion.identity);
    }

    public void FaceRight() {
        bossSprite.transform.localScale = new Vector2(1f, 1f);
    }
    public void FaceLeft() {
        bossSprite.transform.localScale = new Vector2(-1f, 1f);
    }

    public void TurnOnCollider() {
        myCollider.enabled = true;
    }

    public void TurnOffCollider() {
        myCollider.enabled = false;
    }
}
