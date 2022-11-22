using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    [SerializeField] SpriteRenderer[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;
    Animator myAnim;

    private void Awake() { //keeps health going through sewer rooms
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        myAnim = GetComponent<Animator>();

        health = 6; //full health on entry into the sewers
        UpdateHealth();
    }

    private void Update() {
        if (!SceneManager.GetActiveScene().name.Contains("Sewers")) {
            Destroy(gameObject);
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

        if (health > 6) {
            health = 6;
        }
    }

    private void UpdateHealth() {
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
    }
}
