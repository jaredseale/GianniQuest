using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathField : MonoBehaviour
{
    BoxCollider2D myCollider = null;
    PlayerHealth playerHealth = null;
    Player player = null;
    [SerializeField] GameObject crossfade;
    Scene currentScene;
    [SerializeField] bool hasCheckpoint;
    [SerializeField] Vector2 checkpointPos;
 
    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        player = FindObjectOfType<Player>();
        currentScene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerHealth.HurtPlayer(2);

            if (playerHealth.health <= 0) {
                FindObjectOfType<SewersDeathManager>().PlayerDie();
            } else {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                player.Hurt();

                if (hasCheckpoint) {
                    FindObjectOfType<SpawnPosition>().spawnPosition = checkpointPos;
                }

                crossfade.GetComponent<Animator>().SetTrigger("doorTransition");
                StartCoroutine(ReloadScene());
            }

        }
    }

    IEnumerator ReloadScene() {
        if (SceneManager.GetActiveScene().name == "Sewers 8") {
            FindObjectOfType<SpawnPosition>().spawnPosition = new Vector2(-22.54f, 12.27f);
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(currentScene.name);
    }

}
