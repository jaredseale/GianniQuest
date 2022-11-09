using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    [SerializeField] BoxCollider2D playerFeet;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    public bool facingRight;
    public bool jumpAnimationCanBeEnded;
    AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    public bool canMove;
    public bool isOnGround; //used for interacting with things
    [SerializeField] GameObject gianniClone;
    bool hasDoubleJump;
    public bool canDoubleJump;
    [SerializeField] GameObject randall;
    public bool takingDamage;
    public float iframeTime = 0.5f;


    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        facingRight = true;
        jumpAnimationCanBeEnded = false;
        canMove = true;
        takingDamage = false;

        if (PlayerPrefs.GetInt("HasDoubleJump") == 1) {
            hasDoubleJump = true;
            canDoubleJump = true;
        } else {
            hasDoubleJump = false;
            canDoubleJump = false;
        }

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
            if (hasDoubleJump) {
                canDoubleJump = true; //resets double jump
            }
            
        } else {
            isOnGround = false;
        }

        if (PlayerPrefs.GetInt("HasDoubleJump") == 1) { //for when you get the item
            hasDoubleJump = true;
        }

        SpawnClone();

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

        if (!takingDamage) {
            Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed, playerRigidbody.velocity.y);
            playerRigidbody.velocity = playerVelocity;
        }
        

        bool playerIsRunningRight = playerRigidbody.velocity.x > 0.5f;
        playerAnimator.SetBool("RunningRight", playerIsRunningRight);

        bool playerIsRunningLeft = playerRigidbody.velocity.x < -0.5f;
        playerAnimator.SetBool("RunningLeft", playerIsRunningLeft);
    }

    public void Jump() {

        if (Input.GetButtonDown("Jump")) {

            if (!isOnGround && !hasDoubleJump && !canDoubleJump) {
                return;
            } else if (!isOnGround && hasDoubleJump && !canDoubleJump) { //if already double jumped
                return;
            }

            audioSource.PlayOneShot(jumpSound);
            StartCoroutine("JumpAnimationResetDelay");
            //Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            //playerRigidbody.velocity += jumpVelocityToAdd;

            if (!isOnGround) {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed / 1.2f); //little less jump height in the air
            } else {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            }

            if (facingRight) {
                playerAnimator.SetBool("JumpingRight", true);
            } else {
                playerAnimator.SetBool("JumpingLeft", true);
            }

            if (hasDoubleJump && !isOnGround) {
                Instantiate(randall, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1.7f, gameObject.transform.position.z), Quaternion.identity);
                canDoubleJump = false;
            }

        }

        if (Input.GetButtonUp("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed / 3f);
            playerRigidbody.velocity -= jumpVelocityToAdd;
        }
    }

    //the jump animation is ended when the feet hitbox touches the ground again. 
    //this prevents that from happening on the next couple frames when the feet are
    //still in contact with the ground.
    IEnumerator JumpAnimationResetDelay() {
        yield return new WaitForSeconds(0.35f);
        jumpAnimationCanBeEnded = true;
    }

    public void Hurt() {
        StartCoroutine(IFrames());
    }

    public IEnumerator IFrames() {
        takingDamage = true;
        yield return new WaitForSeconds(iframeTime);
        takingDamage = false;
    }

    public void SpawnClone() {
        if (Input.GetKeyDown("q") || Input.GetKeyDown("z")) {
            if (canMove && PlayerPrefs.GetInt("HasCloner") == 1) {
                GameObject[] existingClones = GameObject.FindGameObjectsWithTag("GianniClone");
                foreach (GameObject clone in existingClones) {
                    Destroy(clone);
                }
                Instantiate(gianniClone, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            }
        }
    }


}
