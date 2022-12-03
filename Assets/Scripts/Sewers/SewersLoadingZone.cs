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
    public bool isEnteringRoom;
    SewersLoadingZone[] areaLoadingZones;

    void Start() {
        player = FindObjectOfType<Player>();
        pause = FindObjectOfType<Pause>();
        levelLoader = FindObjectOfType<LevelLoader>();
        isEnteringRoom = true;


        areaLoadingZones = FindObjectsOfType<SewersLoadingZone>();

        StartCoroutine(EnterRoomFailsafe());
    }

    void Update() {
        if (player.inLoadingZone) {
            player.GetComponent<Rigidbody2D>().velocity = playerVelo;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player") && !isEnteringRoom) {

            pause.canPause = false;
            FindObjectOfType<SpawnPosition>().setNextSpawn(nextScenePos.x, nextScenePos.y);
            
            player.inLoadingZone = true;

            switch (direction) {

                case "right":
                    foreach (SewersLoadingZone loadingZone in areaLoadingZones) { //all these foreaches solve a problem where
                                                                                  //other loading zones in the same scene were
                                                                                  //affecting the playerVelo as well
                        loadingZone.playerVelo = new Vector2(8f, -10f);
                    }
                    break;

                case "left":
                    foreach (SewersLoadingZone loadingZone in areaLoadingZones) {
                        loadingZone.playerVelo = new Vector2(-8f, -10f);
                    }
                    break;

                case "up":
                    foreach (SewersLoadingZone loadingZone in areaLoadingZones) {
                        loadingZone.playerVelo = new Vector2(0f, 10f);
                    }
                    break;

                case "down":
                    foreach (SewersLoadingZone loadingZone in areaLoadingZones) {
                        loadingZone.playerVelo = new Vector2(0f, -18f);
                    }
                    break;

                default:
                    Debug.Log("think you typoed the direction my guy");
                    break;
            }

            
            PlayerPrefs.SetInt("SewersLocationDisplay", 0); //hides the location display when moving between sewer rooms
            crossfade.GetComponent<Animator>().SetTrigger("doorTransition");
            levelLoader.loadSceneDelay = 1;
            levelLoader.LoadSceneWithDelay(sceneToLoad, false);

        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            player.inLoadingZone = false;
            if (direction != "down") {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -20f); //stops the player's x, keeps falling speed
            }
            
            isEnteringRoom = false;
        }
    }

    IEnumerator EnterRoomFailsafe() {
        yield return new WaitForSeconds(0.05f); //DON'T USE WAITFORENDOFFRAME IT SUCKS

        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player"))) {
            player.inLoadingZone = true;
            switch (direction) {
                case "right":
                    foreach (SewersLoadingZone loadingZone in areaLoadingZones) {
                        loadingZone.playerVelo = new Vector2(-8f, -10f);
                    }
                    break;

                case "left":
                    foreach (SewersLoadingZone loadingZone in areaLoadingZones) {
                        loadingZone.playerVelo = new Vector2(8f, -10f);
                    }
                    break;

                case "up":
                    foreach (SewersLoadingZone loadingZone in areaLoadingZones) {
                        loadingZone.playerVelo = new Vector2(0f, -20f);
                    }
                    break;

                default:
                    break;
            }
        }

        //this will solve the case where the player is starting from the first room and has no other way to trigger the bool false
        yield return new WaitForSeconds(0.5f);
        isEnteringRoom = false;
    }
}
