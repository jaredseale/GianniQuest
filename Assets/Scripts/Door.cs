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
    [SerializeField] bool fadeOutMusicOnLoad;
    bool pauseCheck;
    [SerializeField] Vector2 nextSpawnPos;

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
        if (Input.GetButtonDown("Up") //if 1) pressed up and 2) on top of loading zone and 3) game not paused
            && myCollider.IsTouchingLayers(LayerMask.GetMask("Player")) 
            && GameObject.Find("Game Manager").GetComponent<Pause>().gamePaused == false) {
            playerController.canMove = false;

            FindObjectOfType<SpawnPosition>().setNextSpawn(nextSpawnPos.x, nextSpawnPos.y);

            transition.SetTrigger("doorTransition");
            audioSource.PlayOneShot(doorOpenSFX);
            levelLoader.LoadSceneWithDelay(sceneToLoad, fadeOutMusicOnLoad);
        }
    }
}
