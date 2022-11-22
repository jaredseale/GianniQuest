﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glimback : MonoBehaviour
{

    public GameObject startPoint;
    public GameObject endPoint;
    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] Animator myAnim;
    [SerializeField] GameObject mySprite;

    public bool idling;

    [SerializeField] GameObject glimback;
    [SerializeField] BoxCollider2D myCollider;
    [SerializeField] AudioSource myAudio;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip bulletSFX;
    Player myPlayer;

    void Start() {
        myPlayer = FindObjectOfType<Player>();

        startPosition = startPoint.transform.position;
        endPosition = endPoint.transform.position;

        if (endPosition.x > startPosition.x) { //flip the sprite if it's walking to the right
            mySprite.transform.localScale = new Vector3(-mySprite.transform.localScale.x, 1f, 1f);
        }

        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
    }

    private void Update() {

        //
        // gun damage
        //

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Bullet"))) {
            myAudio.PlayOneShot(bulletSFX);
        }

        //
        // bomb damage
        //

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Explosion"))) {
            GlimbackDie();
        }


        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {

            if (!myPlayer.takingDamage) { //only hurt if out of iframes
                myPlayer.Hurt();
                FindObjectOfType<PlayerHealth>().HurtPlayer(1);
            }

            Vector2 playerVelo = myPlayer.GetComponent<Rigidbody2D>().velocity;
            if (myPlayer.transform.position.x > gameObject.transform.position.x) { //bounce gianni the other way depending on where he is to the enemy
                myPlayer.playerRigidbody.velocity = new Vector2(10f, 12f);
            } else {
                myPlayer.playerRigidbody.velocity = new Vector2(-10f, 12f);
            }
        }

    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed) {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        /*if (endPosition.x > gameObject.transform.position.x) {
            mySprite.transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            mySprite.transform.localScale = new Vector3(1, 1f, 1f); //flip it
        }*/


        while (glimback.transform.position != target) {
            glimback.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }

        (startPosition, endPosition) = (endPosition, startPosition);
        myAnim.SetTrigger("startTurning"); //turning is handled in the anim
    }

    public void StartWalking() { //driven by anim
        mySprite.transform.localScale = new Vector3(-mySprite.transform.localScale.x, 1f, 1f);
        myAnim.SetTrigger("startWalking");
        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
    }

    void GlimbackDie() {
        myCollider.enabled = false;
        StopAllCoroutines();
        myAnim.SetTrigger("die");
        myAudio.PlayOneShot(deathSFX);
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}