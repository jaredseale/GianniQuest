using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    Player player;
    SpriteRenderer mySprite;
    AudioSource myAudio;
    float xPos;
    float xScale;

    [SerializeField] GameObject[] bullets;
    public bool shootOffCooldown;
    public float shootCooldownLength;
    float bulletSpawnOffset = 1.75f;
    int lastBulletNumber = 0;

    [SerializeField] AudioClip[] gunNotes;
    SewersMusicManager sewerMusic;
    Pause pause;

    void Start() {
        if (PlayerPrefs.GetInt("HasGun") == 0 || !SceneManager.GetActiveScene().name.Contains("Sewers")) { //only allow gun in sewers
            gameObject.SetActive(false);
        }

        player = FindObjectOfType<Player>();
        sewerMusic = FindObjectOfType<SewersMusicManager>();
        pause = FindObjectOfType<Pause>();

        xPos = gameObject.transform.position.x;
        Debug.Log(xPos);
        xScale = gameObject.transform.localScale.x;
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.enabled = false;
        myAudio = GetComponent<AudioSource>();

        shootOffCooldown = true;
    }

    void Update() {

        if (player.facingRight == true) {
            //the random looking xPos offsets are necessary because of the rotations I think
            //note to self: never have a non symmetrical player sprite ever again it makes shit too hard and code too ugly lol
            gameObject.transform.position = new Vector3(Mathf.Abs(xPos + 0.1f) + player.transform.position.x, gameObject.transform.position.y);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            gameObject.transform.localScale = new Vector3(-xScale, gameObject.transform.localScale.y);
        } else {
            gameObject.transform.position = new Vector3(-xPos - 0.1f + player.transform.position.x, gameObject.transform.position.y);
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            gameObject.transform.localScale = new Vector3(Mathf.Abs(xScale), gameObject.transform.localScale.y);
        }

        if (Input.GetMouseButton(1) && player.canMove) {
            mySprite.enabled = true;
            if (shootOffCooldown) {
                shootOffCooldown = false;
                StartCoroutine(ShootCooldownTimer());
                int bulletSelection = RandomBulletGen();

                if (player.facingRight) {
                    //PlayerRecoil("right"); 
                    Instantiate(bullets[bulletSelection], new Vector3(gameObject.transform.position.x + bulletSpawnOffset, 
                        gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                } else {
                    //PlayerRecoil("left");
                    Instantiate(bullets[bulletSelection], new Vector3(gameObject.transform.position.x - bulletSpawnOffset, 
                        gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                }

                PlayGunshot();
                
            }
        }
    }

    /*private void PlayerRecoil(string direction) { //this does not work the way i want it to
        if (direction == "right") {
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 12f);
            player.playerRigidbody.AddForce(new Vector2(-2000f, 0f), ForceMode2D.Force);
        } else if (direction == "left") {
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 12f);
            player.playerRigidbody.AddForce(new Vector2(2000f, 0f), ForceMode2D.Force);
        }
    }*/

    IEnumerator ShootCooldownTimer() {
        yield return new WaitForSeconds(shootCooldownLength);
        shootOffCooldown = true;

        if (!Input.GetMouseButton(1)) {
            mySprite.enabled = false;
        }
    }

    private int RandomBulletGen() {
        int newBulletNumber = Random.Range(0, 9);
        while (newBulletNumber == lastBulletNumber) {
            newBulletNumber = Random.Range(0, 9);
        }

        lastBulletNumber = newBulletNumber;
        return newBulletNumber;
    }

    private void PlayGunshot() {
        if (sewerMusic.beat == 1) {
            myAudio.PlayOneShot(gunNotes[0]);
        } else if (sewerMusic.beat == 2) {
            myAudio.PlayOneShot(gunNotes[1]);
        } else if (sewerMusic.beat == 3) {
            myAudio.PlayOneShot(gunNotes[2]);
        } else if (sewerMusic.beat == 4) {
            myAudio.PlayOneShot(gunNotes[3]);
        }
    }
}
