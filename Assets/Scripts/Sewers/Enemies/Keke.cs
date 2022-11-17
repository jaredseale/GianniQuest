using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keke : MonoBehaviour
{

    public GameObject startPoint;
    public GameObject endPoint;
    public int speed;
    public float idleTimeAnchor;
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] Animator myAnim;
    [SerializeField] GameObject mySprite;

    public bool idling;

    BoxCollider2D myCollider;
    AudioSource myAudio;
    Player myPlayer;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        myAudio = GetComponent<AudioSource>();
        myPlayer = FindObjectOfType<Player>();

        startPosition = startPoint.transform.position;
        endPosition = endPoint.transform.position;

        myAnim.SetTrigger("beginWalking"); //i don't know why this fixes the animation issue but it does

        if (endPosition.x < startPosition.x) { //flip the sprite if it's walking to the left
            mySprite.transform.localScale = new Vector3(-mySprite.transform.localScale.x, 1f, 1f);
        }

        StartCoroutine(IdleTime());
    }

    private void Update() {

        //
        // player jump damage
        //
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerFeet")) && myPlayer.playerRigidbody.velocity.y < 0f) {
            KekeDie();

            //bounce off the enemy when you jump on it
            myPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(myPlayer.GetComponent<Rigidbody2D>().velocity.x, 15f);
        }

        //
        // bomb damage
        //

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Explosion"))) {
            KekeDie();
        }

        //
        // gun damage
        //

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Bullet"))) {
            KekeDie();
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

        if (endPosition.x > gameObject.transform.position.x) {
            mySprite.transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            mySprite.transform.localScale = new Vector3(-1, 1f, 1f); //flip it
        }
        

        while (obj.transform.position != target) {
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }

        (startPosition, endPosition) = (endPosition, startPosition);
        StartCoroutine(IdleTime());
    }

    IEnumerator IdleTime() {
        myAnim.SetTrigger("endWalking");
        idling = true;
        yield return new WaitForSeconds(idleTimeAnchor + Random.Range(-1f, 1f));
        myAnim.SetTrigger("beginWalking");
        idling = false;
        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
    }

    void KekeDie() {
        myCollider.enabled = false;
        StopAllCoroutines();
        myAnim.SetTrigger("kekeDie");
        myAudio.Play();
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
