using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    public GameObject startPoint;
    public GameObject endPoint;
    public int speed;
    public float idleTimeAnchor;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] Animator myAnim;
    [SerializeField] GameObject mySprite;
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject deathTrigger;

    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip dieSFX;

    public bool idling;
    public bool attacking;
    public bool dead;

    [SerializeField] BoxCollider2D myCollider;
    [SerializeField] BoxCollider2D aggroZone;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrowSpawnPos;
    public bool isGraySkeleton;
    public bool isPatrollingSkeleton;
    public float arrowSpeed = 12;
    [SerializeField] AudioSource myAudio;
    Player myPlayer;

    void Start() {
        myPlayer = FindObjectOfType<Player>();

        if (isPatrollingSkeleton) {
            startPosition = startPoint.transform.position;
            endPosition = endPoint.transform.position;

            myAnim.SetTrigger("startWalking"); //i don't know why this fixes the animation issue but it does

            if (endPosition.x < startPosition.x) { //flip the sprite if it's walking to the left
                mySprite.transform.localScale = new Vector3(-mySprite.transform.localScale.x, 1f, 1f);
            }

            idling = true;
            attacking = false;
            dead = false;

            StartCoroutine(IdleTime());
        }
    }

    private void Update() {

        //
        // player jump damage
        //

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerFeet")) && myPlayer.playerRigidbody.velocity.y < 0f) {
            //bounce off the enemy when you jump on it
            myPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(myPlayer.GetComponent<Rigidbody2D>().velocity.x, 15f);

            SkeletonDie();
        }

        //
        // bomb damage
        //

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Explosion"))) {
            SkeletonDie();
        }

        //
        // gun damage
        //

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Bullet"))) {
            SkeletonDie();
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

        if (!attacking && !dead && aggroZone.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            Attack();
        }

        if (attacking && myPlayer.transform.position.x > skeleton.transform.position.x) {
            skeleton.transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (attacking && myPlayer.transform.position.x < skeleton.transform.position.x) {
            skeleton.transform.localScale = new Vector3(-1f, 1f, 1f); //flip it
        }
    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed) {
        Vector3 curStartPosition = obj.transform.position;
        float time = 0f;

        if (endPosition.x > gameObject.transform.position.x) {
            skeleton.transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            skeleton.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        while (obj.transform.position != target) {
            obj.transform.position = Vector3.Lerp(curStartPosition, target, (time / Vector3.Distance(curStartPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }

        (startPosition, endPosition) = (endPosition, startPosition);
        StartCoroutine(IdleTime());
    }

    IEnumerator IdleTime() {
        myAnim.SetTrigger("startIdling");
        idling = true;
        yield return new WaitForSeconds(idleTimeAnchor + Random.Range(-1f, 1f));
        myAnim.SetTrigger("startWalking");
        idling = false;
        StartCoroutine(Vector3LerpCoroutine(skeleton, endPosition, speed));
    }


    private void Attack() {
        StopAllCoroutines();
        attacking = true;
        idling = false;
        myAnim.SetTrigger("startShooting");
    }

    public void FireArrow() { //driven by anim
        //when the skeleton is facing left, the arrow spawns too far to the right, and i can't figure out how to fix that for the life of me
        myAudio.PlayOneShot(shootSFX);
        GameObject spawnedArrow = Instantiate(arrow, arrowSpawnPos.transform.position, Quaternion.identity);
        SpriteRenderer arrowSprite = spawnedArrow.GetComponentInChildren<SpriteRenderer>();
        arrowSprite.transform.localScale = new Vector3(skeleton.transform.localScale.x, 1f, 1f);
        if (!isGraySkeleton) {
            spawnedArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * skeleton.transform.localScale.x, 0f);
        } else { //gray skeleton shoots faster arrows
            spawnedArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * skeleton.transform.localScale.x * 1.5f, 0f);
        }

    }

    public void EndAttack() { //driven by anim
        attacking = false;
        idling = true;

        myAnim.SetTrigger("startIdling");

        if (aggroZone.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            Attack();
        } else {
            if (isPatrollingSkeleton) {
                myAnim.SetTrigger("startWalking");
                StartCoroutine(Vector3LerpCoroutine(skeleton, endPosition, speed));
            }
        }
        
    }

    void SkeletonDie() {

        if (deathTrigger != null) {
            Destroy(deathTrigger);
        }

        myCollider.enabled = false;
        StopAllCoroutines();
        dead = true;
        StartCoroutine(TimeOut());
        myAnim.SetTrigger("die");
        myAudio.PlayOneShot(dieSFX);
    }

    IEnumerator TimeOut() {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
