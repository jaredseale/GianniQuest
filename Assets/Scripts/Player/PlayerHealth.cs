using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    [SerializeField] SpriteRenderer[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;
    Animator myAnim;

    private void Awake() { //keeps health going through rooms
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        myAnim = GetComponent<Animator>();

        health = 6; //full health on entry into the sewers
    }

    private void Update() {

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

    public void HurtPlayer(int hurtAmount) {
        myAnim.SetTrigger("shakeHealth"); 
        health -= hurtAmount;

        if (health < 0) {
            health = 0;
        }

        if (health == 0) {
            //die
        }
    }

    public void HealPlayer(int healAmount) {
        health += healAmount;

        if (health > 6) {
            health = 6;
        }
    }
}
