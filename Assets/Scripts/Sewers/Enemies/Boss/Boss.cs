using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//most of the methods below are called by the animator

public class Boss : MonoBehaviour
{

    public int bossHealth;
    public int bossMaxHealth;

    [SerializeField] GameObject homingBirds;
    [SerializeField] GameObject bossEnemies;
    [SerializeField] GameObject bossSprite;
    [SerializeField] GameObject attack5;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject healthBarUI;
    [SerializeField] GameObject bossDialogue;

    [Space(20)]

    [SerializeField] AudioClip wingFlap;
    [SerializeField] AudioClip enemyPoof;
    [SerializeField] AudioClip birdPass;
    [SerializeField] AudioClip attack1Tele;
    [SerializeField] AudioClip attack2Tele;
    [SerializeField] AudioClip attack3Tele;
    [SerializeField] AudioClip attack4Tele;

    int attackTurnNumber;
    public int attackID;
    int lastAttackID;

    CircleCollider2D myCollider;
    Player player;
    Animator myAnim;
    AudioSource myAudio;
    Pause pause;

    void OnEnable() {
        myCollider = GetComponent<CircleCollider2D>();
        player = FindObjectOfType<Player>();
        pause = FindObjectOfType<Pause>();
        myAnim = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        attackTurnNumber = 1;

        if (PlayerPrefs.GetInt("BossCheckpoint") == 0) {
            bossHealth = bossMaxHealth;
        } else {
            bossHealth = bossMaxHealth / 2;
        }
        
        healthBar.fillAmount = 1f;
    }

    private void Update() {

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ((float) bossHealth / (float) bossMaxHealth), Time.deltaTime);

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {

            if (!player.takingDamage) { //only hurt if out of iframes
                player.Hurt();
                FindObjectOfType<PlayerHealth>().HurtPlayer(1);
            }

            Vector2 playerVelo = player.GetComponent<Rigidbody2D>().velocity;
            if (player.transform.position.x > gameObject.transform.position.x) { //bounce gianni the other way depending on where he is to the enemy
                player.playerRigidbody.velocity = new Vector2(10f, 12f);
            } else {
                player.playerRigidbody.velocity = new Vector2(-10f, 12f);
            }
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Bullet"))) {
            TakeDamage(1);
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Explosion"))) {
            TakeDamage(3);
        }

        if (bossHealth <= 0) {
            attackID = 99;
            myAnim.SetInteger("Attack", attackID);
        }
    }

    public void TakeDamage(int damage) {
        bossHealth -= damage;

        if (bossHealth < bossMaxHealth / 2) {
            PlayerPrefs.SetInt("BossCheckpoint", 1);
        }

        if (bossHealth <= 0) {
            attackID = 99;
        } else {
            StartCoroutine(HurtBoss());
        }
    }

    IEnumerator HurtBoss() {
        myCollider.enabled = false;
        bossSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        myCollider.enabled = true;
        bossSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        yield return null;
    }

    public void EndFight() {
    
    }

    public void RollNextAttack() {
        if (attackTurnNumber % 5f == 0f) {
            attackID = 1;
            lastAttackID = 1;
        } else {
            while (attackID == lastAttackID) {
                attackID = Random.Range(2, 6);
            }

            lastAttackID = attackID;
        }

        myAnim.SetInteger("Attack", attackID);
        attackTurnNumber++;
    }

    public void PlayTelegraphSound() {
        myAudio.pitch = 1f;

        if (attackID == 1) {
            myAudio.PlayOneShot(attack1Tele);
        } else if (attackID == 2) {
            myAudio.PlayOneShot(attack2Tele);
        } else if (attackID == 3) {
            myAudio.PlayOneShot(attack3Tele);
        } else if (attackID == 4) {
            myAudio.PlayOneShot(attack4Tele);
        } else if (attackID == 5) {

        }
    }

    public void SpawnBossEnemies() { //Attack 1
        myAudio.PlayOneShot(enemyPoof);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("BossEnemies");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        Instantiate(bossEnemies, new Vector2(0f, 0f), Quaternion.identity);
    }

    public void SpawnHomingBirds() { //Attack 3
        Instantiate(homingBirds, new Vector2(0f, 0f), Quaternion.identity);
    }

    public void Attack2Pass() {
        myAudio.pitch = Random.Range(0.7f, 1.1f);
        myAudio.PlayOneShot(birdPass);
    }

    public void Attack5() {
        Instantiate(attack5, new Vector2(0f, 0f), Quaternion.identity);
    }

    public void FacePlayer() {
        if (player.gameObject.transform.position.x > gameObject.transform.position.x) {
            FaceRight();
        } else {
            FaceLeft();
        }
    }

    public void FaceRight() {
        bossSprite.transform.localScale = new Vector2(1f, 1f);
    }
    public void FaceLeft() {
        bossSprite.transform.localScale = new Vector2(-1f, 1f);
    }

    public void TurnOnCollider() { //for the entrance anim and also post fight
        myCollider.enabled = true;
    }

    public void TurnOffCollider() {
        myCollider.enabled = false;
    }

    public void PlayWingFlap() {
        myAudio.PlayOneShot(wingFlap);
    }

    public void HealthBarEnter() {
        healthBarUI.GetComponent<Animator>().SetTrigger("healthBarEnter");
    }

    public void HealthBarExit() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("BossEnemies");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        healthBarUI.GetComponent<Animator>().SetTrigger("healthBarExit");
    }

    public void EndFlapping() {
        bossSprite.GetComponent<Animator>().SetTrigger("endFight");
    }

    public void EnableDialogue() {
        bossDialogue.SetActive(true);
    }

    public void BlackenSprite() {
        bossSprite.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
    }

    public void UnblackenSprite() {
        bossSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    public void EnablePause() {
        pause.canPause = true;
    }

    public void DisablePause() {
        pause.canPause = false;
    }
}
