using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] GameObject damageRadius;
    [SerializeField] GameObject explosionAnim;
    [SerializeField] GameObject parentObject;
    [SerializeField] BoxCollider2D bombCollider;
    [SerializeField] BoxCollider2D bottomCollider;
    [SerializeField] CircleCollider2D triggerCollider;
    Rigidbody2D parentRB;

    [SerializeField] AudioClip beep;
    [SerializeField] AudioClip explosionSFX;
    AudioSource myAudio;
    Player myPlayer;
    bool playerLeftCollider;

    //TODO: have the bomb interact with destructible walls when you get to that point in dev

    void Start() {
        myAudio = GetComponent<AudioSource>();
        myPlayer = FindObjectOfType<Player>();
        StartCoroutine(DelayedCollider());
        parentRB = parentObject.GetComponent<Rigidbody2D>();
        playerLeftCollider = false;
    }

    private void Update() {

        if (bottomCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !playerLeftCollider) {
            parentRB.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (damageRadius.GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player"))) {

            if (!myPlayer.takingDamage) { //only hurt if out of iframes
                myPlayer.Hurt();
                FindObjectOfType<PlayerHealth>().HurtPlayer(2);
            }

            if (myPlayer.transform.position.x > parentObject.transform.position.x) { //bounce gianni the other way depending on where he is to the enemy
                myPlayer.playerRigidbody.velocity = new Vector2(10f, 12f);
            } else {
                myPlayer.playerRigidbody.velocity = new Vector2(-10f, 12f);
            }
        }
    }

    IEnumerator DelayedCollider() {
        yield return new WaitForSeconds(0.05f);

        while (true) {
            if (!triggerCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
                playerLeftCollider = true;
                parentRB.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                bombCollider.enabled = true;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Explode() { //called from anim
        StopAllCoroutines();
        bombCollider.enabled = false;
        parentRB.constraints = RigidbodyConstraints2D.FreezeAll;
        myAudio.PlayOneShot(explosionSFX);
        damageRadius.SetActive(true);
        explosionAnim.SetActive(true);
    }

    public void DisableDamageRadius() { //called from anim
        damageRadius.SetActive(false);
    }

    public void Delete() { //called from anim
        Destroy(parentObject);
    }

    public void Beep() { //called from anim
        myAudio.PlayOneShot(beep);
    }

}
