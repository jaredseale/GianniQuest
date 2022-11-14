using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameObject bat;
    public GameObject startPoint;
    public GameObject attackPoint;
    public GameObject endPoint;
    public BoxCollider2D aggroZone;
    public int speed;
    private Vector3 startPosition;
    private Vector3 attackPosition;
    private Vector3 endPosition;
    public List<Vector3> waypoints;
    [SerializeField] Animator myAnim;
    [SerializeField] GameObject mySprite;

    public bool attacking;

    public int batHealth = 2;

    BoxCollider2D myCollider;
    AudioSource myAudio;
    Player myPlayer;

    [SerializeField] AudioClip hurtSFX;
    [SerializeField] AudioClip dieSFX;

    void Start() {
        myCollider = bat.GetComponent<BoxCollider2D>();
        myAudio = GetComponent<AudioSource>();
        myPlayer = FindObjectOfType<Player>();

        myAudio.volume = 0f;
        attacking = false;
        startPosition = startPoint.transform.position;
        attackPosition = attackPoint.transform.position;
        endPosition = endPoint.transform.position;
        waypoints = new List<Vector3>(){startPosition, attackPosition, endPosition};

        if (endPosition.x < startPosition.x) { //flip the sprite if it's going right to left
            mySprite.transform.localScale = new Vector3(-mySprite.transform.localScale.x, 1f, 1f);
        }
    }

    private void Update() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerFeet")) && myPlayer.playerRigidbody.velocity.y < 0f) {
            batHealth -= 1;

            if (batHealth == 0) {
                BatDie();
            } else {
                StartCoroutine(HurtBat());
            }

            //bounce off the enemy when you jump on it
            myPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(myPlayer.GetComponent<Rigidbody2D>().velocity.x, 15f);
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) { //hurt the player

            if (!myPlayer.takingDamage) { //only hurt if out of iframes
                myPlayer.Hurt();
                FindObjectOfType<PlayerHealth>().HurtPlayer(1);
            }

            Vector2 playerVelo = myPlayer.GetComponent<Rigidbody2D>().velocity;
            if (myPlayer.transform.position.x > bat.transform.position.x) { //bounce gianni the other way depending on where he is to the enemy
                myPlayer.playerRigidbody.velocity = new Vector2(10f, 12f);
            } else {
                myPlayer.playerRigidbody.velocity = new Vector2(-10f, 12f);
            }
        }

        if (aggroZone.IsTouchingLayers(LayerMask.GetMask("Player")) && !attacking) {
            StartCoroutine(BatAttack(waypoints, speed));
        }

        if (attacking) { //only play the looping attack audio if the bat is attacking
            myAudio.volume = 1f;
        } else {
            myAudio.volume = 0f;
        }

    }

    IEnumerator BatAttack(List<Vector3> waypoints, float speed) {
        attacking = true;
        myAnim.SetTrigger("beginAttack");
        //myAudio.PlayOneShot(attackSFX);

        foreach (var waypoint in waypoints) {
            while ((bat.transform.position - waypoint).sqrMagnitude > 0.001f) {
                Debug.Log("moving to waypoint: " + waypoint);
                bat.transform.position = Vector3.MoveTowards(
                          bat.transform.position,
                          waypoint,
                          speed * Time.deltaTime
                );
                // Wait a frame, 
                // and resume the next loop iteration next frame.
                yield return null;
            }
        }

        attacking = false;
        myAnim.SetTrigger("returnToIdle");
        (waypoints[0], waypoints[2]) = (waypoints[2], waypoints[0]); //make the new starts and ends the opposite of what they are

        if (waypoints[2].x < bat.transform.position.x) { //flip the sprite if it's going right to left
            mySprite.transform.localScale = new Vector3(Mathf.Abs(mySprite.transform.localScale.x), 1f, 1f);
        } else {
            mySprite.transform.localScale = new Vector3((-mySprite.transform.localScale.x), 1f, 1f);
        }
    }


    void BatDie() {
        myAudio.enabled = false;
        StopAllCoroutines();
        myCollider.enabled = false;
        myAnim.SetTrigger("die");
        bat.GetComponent<AudioSource>().PlayOneShot(dieSFX);
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator HurtBat() {
        bat.GetComponent<AudioSource>().PlayOneShot(hurtSFX);
        myAnim.SetTrigger("takeDamage");
        myCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        myCollider.enabled = true;
        myAnim.SetTrigger("beginAttack");
    }
}
