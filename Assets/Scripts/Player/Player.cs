﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Rigidbody2D playerRigidbody;
    Pause pause;
    [SerializeField] SpriteRenderer playerSprite;
    Animator playerAnimator;
    [SerializeField] BoxCollider2D playerFeet;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    public bool facingRight;
    public bool jumpAnimationCanBeEnded;
    AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip hurtSound;
    public bool canMove;
    public bool isOnGround; //used for interacting with things
    [SerializeField] GameObject gianniClone;
    [SerializeField] GameObject bomb;
    bool hasDoubleJump;
    public bool canDoubleJump;
    [SerializeField] GameObject randall;
    public bool takingDamage;
    public float iframeTime = 0.5f;
    [SerializeField] SewersDeathManager deathManager;
    [SerializeField] GameObject map;
    public bool mapOpen;
    public bool inLoadingZone;
    public bool isInvulnerable;


    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        pause = FindObjectOfType<Pause>();
        facingRight = true;
        jumpAnimationCanBeEnded = false;
        canMove = true;
        takingDamage = false;
        inLoadingZone = false;
        mapOpen = false;
        isInvulnerable = false;

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

        LogSewerMap();

    }

    void Update() {

        Map();

        if (canMove && pause.gamePaused == false) { //currently used by loading zones to prevent movement while loading is happening
            Run();
            Jump();
            SpawnClone();
            SetBomb();
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

        if (PlayerPrefs.GetInt("HasDoubleJump") == 1) { //for the moment when you get the cloud
            hasDoubleJump = true;
        }

        if (gameObject.transform.position.y < -500f) {
            gameObject.transform.position = FindObjectOfType<SpawnPosition>().spawnPosition;
            playerRigidbody.velocity = new Vector2(0f, 0f);
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
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if (controlThrow > 0.5f) {
                facingRight = true;
            } else if (controlThrow < -0.5f) {
                facingRight = false;
            } 
        }

        if (!takingDamage && !inLoadingZone) {
            Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed, playerRigidbody.velocity.y);
            playerRigidbody.velocity = playerVelocity;
        }
        

        bool playerIsRunningRight = playerRigidbody.velocity.x > 0.5f;
        playerAnimator.SetBool("RunningRight", playerIsRunningRight);

        bool playerIsRunningLeft = playerRigidbody.velocity.x < -0.5f;
        playerAnimator.SetBool("RunningLeft", playerIsRunningLeft);
    }

    public void Jump() {

        if (Input.GetButtonDown("Jump") && !inLoadingZone) {

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
        if (!isInvulnerable) {
            audioSource.PlayOneShot(hurtSound);
            StartCoroutine(IFrames());
            StartCoroutine(DamageBlink());
        }
    }

    public IEnumerator IFrames() {
        takingDamage = true;
        yield return new WaitForSeconds(iframeTime);
        takingDamage = false;
    }

    public IEnumerator DamageBlink() {
        float blinkSpeed = 5f;
        for (float i = 0; i < blinkSpeed; i++) {
            playerSprite.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(iframeTime / blinkSpeed / 2f);
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(iframeTime / blinkSpeed / 2f);
        }
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

    public void SetBomb() {
        if (Input.GetKeyDown("e") || Input.GetKeyDown("c")) {
            if (canMove && PlayerPrefs.GetInt("HasBomb") == 1 && FindObjectOfType<Bomb>() == null 
                && SceneManager.GetActiveScene().name.Contains("Sewers")) {
                Instantiate(bomb, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), Quaternion.identity);
            }
        }
    }

    public void Map() {
        if (Input.GetKeyDown("m")) {
            if (canMove && PlayerPrefs.GetInt("HasDoubleJump") == 1 && mapOpen == false
                && SceneManager.GetActiveScene().name.Contains("Sewers")) {
                map.SetActive(true);
                pause.canPause = false;
                mapOpen = true;
            } else if (mapOpen) {
                map.SetActive(false);
                pause.canPause = true;
                mapOpen = false;
            }
        }
    }

    private void LogSewerMap() {
        if (SceneManager.GetActiveScene().name.Contains("Sewers")) {
            string sceneName = SceneManager.GetActiveScene().name;
            switch (sceneName) {
                case "Sewers 9":
                    PlayerPrefs.SetInt("SMRoom9", 1);
                    break;

                case "Sewers 10":
                    PlayerPrefs.SetInt("SMRoom10", 1);
                    break;

                case "Sewers 11":
                    PlayerPrefs.SetInt("SMRoom11", 1);
                    break;

                case "Sewers 12":
                    PlayerPrefs.SetInt("SMRoom12", 1);
                    break;

                case "Sewers 13":
                    PlayerPrefs.SetInt("SMRoom1314", 1);
                    break;

                case "Sewers 15":
                    PlayerPrefs.SetInt("SMRoom15", 1);
                    break;

                case "Sewers 16":
                    PlayerPrefs.SetInt("SMRoom16", 1);
                    break;

                default:
                    break;
            }
        }
    }


}
