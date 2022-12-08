using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    int maxHealth;
    [SerializeField] SpriteRenderer[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;
    Animator myAnim;
    Canvas canvas = null;

    private void Awake() { //keeps health going through sewer rooms
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        myAnim = GetComponent<Animator>();
        canvas = GetComponent<Canvas>();

        maxHealth = PlayerPrefs.GetInt("MaxHealth");
        health = maxHealth; //full health on entry into the sewers

        if (maxHealth < 8) {
            hearts[3].gameObject.SetActive(false);
        }

        if (maxHealth < 10) {
            hearts[4].gameObject.SetActive(false);
        }

        UpdateHealth();
    }

    private void Update() {
        if (!SceneManager.GetActiveScene().name.Contains("Sewers")) {
            Destroy(gameObject);
        }

        if (canvas.worldCamera == null && SceneManager.GetActiveScene().name != "Overworld") { //this re-hooks up the health to the screen when the current scene is reloaded
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }

    public void HurtPlayer(int hurtAmount) {
        myAnim.SetTrigger("shakeHealth"); 
        health -= hurtAmount;

        UpdateHealth();

        if (health < 0) {
            health = 0;
        }

        if (health <= 0) {
            //die
            FindObjectOfType<SewersDeathManager>().PlayerDie();
            Destroy(gameObject); //forces it to come back upon respawning
        }
    }

    public void HealPlayer(int healAmount) {
        health += healAmount;

        UpdateHealth();

        if (health > maxHealth) {
            health = 6;
        }
    }

    private void UpdateHealth() { // i am fully aware this is a gross way of doing this but i don't want to simplify it if i'm only using it in this script OKAY???
        foreach (SpriteRenderer heart in hearts) {
            heart.sprite = emptyHeart;
        }

        if (health >= 1) {
            hearts[0].sprite = halfHeart;
        }

        if (health >= 2) {
            hearts[0].sprite = fullHeart;
        }

        if (health >= 3) {
            hearts[1].sprite = halfHeart;
        }

        if (health >= 4) {
            hearts[1].sprite = fullHeart;
        }

        if (health >= 5) {
            hearts[2].sprite = halfHeart;
        }

        if (health >= 6) {
            hearts[2].sprite = fullHeart;
        }

        if (health >= 7) {
            hearts[3].sprite = halfHeart;
        }

        if (health >= 8) {
            hearts[3].sprite = fullHeart;
        }

        if (health >= 9) {
            hearts[4].sprite = halfHeart;
        }

        if (health >= 10) {
            hearts[4].sprite = fullHeart;
        }
    }

    public void IncreaseMaxHealth() {
        PlayerPrefs.SetInt("MaxHealth", (maxHealth + 2));
        maxHealth = PlayerPrefs.GetInt("MaxHealth");
        health = maxHealth;

        if (health >= 8) {
            hearts[3].gameObject.SetActive(true);
        }

        if (health >= 10) {
            hearts[4].gameObject.SetActive(true);
        }

        UpdateHealth();
    }
}
