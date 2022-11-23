using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SewersLoadingZone : MonoBehaviour
{
    [SerializeField] string direction;
    [SerializeField] string sceneToLoad;
    [SerializeField] Vector2 nextScenePos;
    [SerializeField] GameObject crossfade;
    Player player = null;
    Pause pause = null;
    Vector2 playerVelo;
    LevelLoader levelLoader;


    void Start() {
        player = FindObjectOfType<Player>();
        pause = FindObjectOfType<Pause>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    void Update() {
        if (player.inLoadingZone) {
            player.GetComponent<Rigidbody2D>().velocity = playerVelo;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player")) {

            pause.canPause = false;
            FindObjectOfType<SpawnPosition>().setNextSpawn(nextScenePos.x, nextScenePos.y);
            player.inLoadingZone = true;

            switch (direction) {

                case "right":
                    playerVelo = new Vector2(8f, -10f);
                    break;

                case "left":
                    playerVelo = new Vector2(-8f, -10f);
                    break;

                case "up":
                    playerVelo = new Vector2(0f, 8f);
                    break;

                case "down":
                    //let gravity do the job baby
                    break;

                default:
                    Debug.Log("think you typed in the direction wrong my guy");
                    break;
            }

            
            PlayerPrefs.SetInt("SewersLocationDisplay", 0); //hides the location display when moving between sewer rooms
            crossfade.GetComponent<Animator>().SetTrigger("doorTransition");
            levelLoader.loadSceneDelay = 1;
            levelLoader.LoadSceneWithDelay(sceneToLoad, false);

        }
    }
}
