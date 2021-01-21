using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    BoxCollider2D myCollider;
    LevelLoader levelLoader;
    AudioSource audioSource;
    [SerializeField] GameObject arrow;
    [SerializeField] AudioClip doorOpenSFX;
    Player playerController;
         
    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        levelLoader = FindObjectOfType<LevelLoader>();
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<Player>();
    }

    void Update() {
        ArrowBounce();
        LoadNewScene();
    }

    private void ArrowBounce() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            arrow.SetActive(true);
        } else {
            arrow.SetActive(false);
        }
    }

    private void LoadNewScene() {
        if (Input.GetKeyDown("up")) {
            playerController.canMove = false;
            audioSource.PlayOneShot(doorOpenSFX);
            levelLoader.LoadSceneWithDelay(sceneToLoad);
        }
    }
}
