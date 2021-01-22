using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    LevelLoader levelLoader;
    AudioSource audioSource;
    [SerializeField] AudioClip doorOpenSFX;
    Player playerController;
    [SerializeField] Animator transition;
    BoxCollider2D myCollider;

    void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<Player>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        LoadNewScene();
    }

    private void LoadNewScene() {
        if (Input.GetButtonDown("Up") && myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) { //if pressed up and on top of loading zone
            playerController.canMove = false;
            transition.SetTrigger("doorTransition");
            audioSource.PlayOneShot(doorOpenSFX);
            levelLoader.LoadSceneWithDelay(sceneToLoad, false);
        }
    }
}
