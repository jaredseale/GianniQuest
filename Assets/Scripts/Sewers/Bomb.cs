using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] GameObject damageRadius;
    [SerializeField] GameObject explosionAnim;
    [SerializeField] GameObject parentObject;

    [SerializeField] AudioClip beep;
    [SerializeField] AudioClip explosionSFX;
    AudioSource myAudio;
    Player myPlayer;

    //TODO: have the bomb interact with destructible walls when you get to that point in dev

    void Start() {
        myAudio = GetComponent<AudioSource>();
        myPlayer = FindObjectOfType<Player>();
        
    }

    private void Update() {

        if (parentObject.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            parentObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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

    public void Explode() { //called from anim

        parentObject.GetComponent<BoxCollider2D>().enabled = false;

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
