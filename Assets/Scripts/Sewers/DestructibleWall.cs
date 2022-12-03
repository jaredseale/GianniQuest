using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{

    [SerializeField] ParticleSystem dust;
    [SerializeField] string playerPrefName; //0 = intact, 1 = destroyed
    Animator myAnim;
    AudioSource myAudio;
    BoxCollider2D myCollider;

    void Start() {
        if (PlayerPrefs.GetInt(playerPrefName) == 1) {
            Destroy(gameObject);
        }

        myAnim = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        myCollider = GetComponent<BoxCollider2D>();

    }

    void Update() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Explosion"))) {
            Crumble();
        }

    }

    private void Crumble() {
        dust.Play();
        myAnim.SetTrigger("crumble");
        myAudio.Play();
        myCollider.enabled = false;
        PlayerPrefs.SetInt(playerPrefName, 1);
    }
}
