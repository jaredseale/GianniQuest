using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    BoxCollider2D playerFeet;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    public bool facingRight;
    public bool jumpAnimationCanBeEnded;
    AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    public bool canMove;
    public bool isOnGround; //used for interacting with things

    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerFeet = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        facingRight = true;
        jumpAnimationCanBeEnded = false;
        canMove = true;

        float spawnXPos = FindObjectOfType<SpawnPosition>().spawnPosition.x;
        float spawnYPos = FindObjectOfType<SpawnPosition>().spawnPosition.y;
        Vector2 playerPos = new Vector2(spawnXPos, spawnYPos);
        gameObject.transform.position = playerPos;

    }

    void Update() {
        if (canMove && GameObject.Find("Game Manager").GetComponent<Pause>().gamePaused == false) { //currently used by loading zones to prevent movement while loading is happening
            Run();
            Jump();
        } else {
            playerRigidbody.velocity = new Vector2(0f, 0f);
            playerAnimator.SetBool("RunningRight", false);
            playerAnimator.SetBool("RunningLeft", false);
        }

        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            isOnGround = true;
        } else {
            isOnGround = false;
        }

    }

    public void Run() {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && jumpAnimationCanBeEnded == true) {
            playerAnimator.SetBool("JumpingLeft", false);
            playerAnimator.SetBool("JumpingRight", false);
            jumpAnimationCanBeEnded = false;
        }

        float controlThrow = Input.GetAxisRaw("Horizontal");

        //to determine the direction the player should be facing in the jump animation
        if (controlThrow > 0.5f) {
            facingRight = true;
        } else if (controlThrow < -0.5f) {
            facingRight = false;
        }

        Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;

        bool playerIsRunningRight = playerRigidbody.velocity.x > 0.5f;
        playerAnimator.SetBool("RunningRight", playerIsRunningRight);

        bool playerIsRunningLeft = playerRigidbody.velocity.x < -0.5f;
        playerAnimator.SetBool("RunningLeft", playerIsRunningLeft);
    }

    public void Jump() {

        if (!isOnGround) {
            return;
        }

        if (Input.GetButtonDown("Jump")) {
            audioSource.PlayOneShot(jumpSound);
            StartCoroutine("JumpAnimationResetDelay");
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            playerRigidbody.velocity += jumpVelocityToAdd;

            if (facingRight) {
                playerAnimator.SetBool("JumpingRight", true);
            } else {
                playerAnimator.SetBool("JumpingLeft", true);
            }

        }
    }

    //the jump animation is ended when the feet hitbox touches the ground again. 
    //this prevents that from happening on the next couple frames when the feet are
    //still in contact with the ground.
    IEnumerator JumpAnimationResetDelay() {
        yield return new WaitForSeconds(0.35f);
        jumpAnimationCanBeEnded = true;
    }


}
