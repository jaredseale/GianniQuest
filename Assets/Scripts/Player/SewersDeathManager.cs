using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SewersDeathManager : MonoBehaviour
{
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] BoxCollider2D playerCollider;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] CinemachineVirtualCamera cmCamera;
    [SerializeField] Animator crossfade;

    Pause pause = null;
 
    void Start() {
        pause = FindObjectOfType<Pause>();
    }

    public void PlayerDie() {
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        playerCollider.enabled = false;
        pause.canPause = false;
        playerAudio.PlayOneShot(deathSFX);
        StartCoroutine(DeathLerp());

    }

    public void LoadSewersRoom1() {
        PlayerPrefs.SetInt("SewersLocationDisplay", 1); //sets the location text for the sewers to show the next time you enter
        FindObjectOfType<SpawnPosition>().overworldSpawnPosition = "sewer";
        FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Overworld", true);
    }

    IEnumerator DeathLerp() {
        float timeElapsed = 0;
        float lerpDuration = 1.5f;
        while (timeElapsed < lerpDuration) {
            playerSprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, timeElapsed / lerpDuration));
            cmCamera.m_Lens.OrthographicSize = Mathf.Lerp(7.5f, 2f, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerSprite.color = new Color(1f, 1f, 1f, 0f);
        cmCamera.m_Lens.OrthographicSize = 2f;
        crossfade.SetTrigger("levelTransition");
        LoadSewersRoom1();
    }


}
