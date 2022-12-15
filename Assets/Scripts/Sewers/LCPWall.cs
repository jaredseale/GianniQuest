using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCPWall : MonoBehaviour
{

    [SerializeField] string playerPrefName; //0 = intact, 1 = destroyed
    AudioSource myAudio;
    BoxCollider2D myCollider;
    [SerializeField] GameObject entry;
    [SerializeField] GameObject dimHoleSprite;

    void Start() {
        myAudio = GetComponent<AudioSource>();
        myCollider = GetComponent<BoxCollider2D>();

        if (PlayerPrefs.GetInt(playerPrefName) == 1) {
            entry.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Interactable>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            myCollider.enabled = false;
        }

        if (PlayerPrefs.GetInt("BrokeOrangeEgg") == 1) {
            dimHoleSprite.SetActive(true);
        }

        

    }

    void Update() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Explosion"))) {
            Crumble();
        }

    }

    private void Crumble() {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Interactable>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        myAudio.Play();
        myCollider.enabled = false;
        entry.SetActive(true);
        PlayerPrefs.SetInt(playerPrefName, 1);
    }
}
