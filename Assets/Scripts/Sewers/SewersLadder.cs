using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SewersLadder : MonoBehaviour
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
    [SerializeField] string overworldSpawnPos;
    [SerializeField] float ladderLoadSceneDelay = 4f;
    bool transitionInProgress;
    Player player;

    void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<Player>();
        myCollider = GetComponent<BoxCollider2D>();
        transitionInProgress = false;
        player = FindObjectOfType<Player>();
    }

    void Update() {
        if (!transitionInProgress) {
            LoadNewScene();
        }
    }

    private void LoadNewScene() {
        if (Input.GetButtonDown("Up") //if 1) pressed up and 2) on top of loading zone and 3) game not paused and 4) not in the air
            && myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))
            && GameObject.Find("Game Manager").GetComponent<Pause>().gamePaused == false
            && playerController.isOnGround) {
            playerController.canMove = false;
            transitionInProgress = true;
            player.isInvulnerable = true;

            levelLoader.loadSceneDelay = ladderLoadSceneDelay;
            FindObjectOfType<SpawnPosition>().setNextSpawn(nextSpawnPos.x, nextSpawnPos.y);
            FindObjectOfType<SpawnPosition>().overworldSpawnPosition = overworldSpawnPos;

            PlayerPrefs.SetInt("SewersLocationDisplay", 1); //sets the location text for the sewers to show the next time you enter

            transition.SetTrigger("doorTransition");
            audioSource.PlayOneShot(doorOpenSFX);
            levelLoader.LoadSceneWithDelay(sceneToLoad, fadeOutMusicOnLoad);
        }
    }
}
