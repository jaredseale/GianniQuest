using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dollar : MonoBehaviour
{

    [SerializeField] GameObject dollar;
    [SerializeField] GameObject dollarGetText;
    [SerializeField] AudioClip chaChing;
    Player myPlayer;
    DialogueManager myDialogueManager;
    public int dollarCutsceneState; //1 = not started 2 = waiting 3 = ready to go back to 1

    private void Start() {
        myPlayer = FindObjectOfType<Player>();
        myDialogueManager = FindObjectOfType<DialogueManager>(); //this is to make the arrow go away temporarily
        dollarCutsceneState = 1;
    }

    private void Update() {
        if (dollarCutsceneState == 3) {
            RegainMovement();
        }
    }

    public void CollectDollar() {

        dollarCutsceneState = 2;
        myDialogueManager.inDialogue = true;
        dollar.SetActive(true);
        StartCoroutine("DelayedText");
        myPlayer.canMove = false;
        PlayerPrefs.SetInt("Dollars", PlayerPrefs.GetInt("Dollars") + 1);
    }

    IEnumerator DelayedText() {

        yield return new WaitForSeconds(4f);
        dollarGetText.SetActive(true);
        this.GetComponent<AudioSource>().PlayOneShot(chaChing);
        dollarCutsceneState = 3;
    }

    IEnumerator PutAwayDollar() {
        if (PlayerPrefs.GetInt("Dollars") == 3) {
            StartCoroutine("StartIntermission");
        }

        dollar.GetComponent<Animator>().SetTrigger("CollectDollar");
        yield return new WaitForSeconds(3f);
        dollar.SetActive(false);
    }

    void RegainMovement() {

        if (dollarCutsceneState == 3) {
            float controlThrow = Input.GetAxisRaw("Horizontal");

            if (controlThrow > 0.5f || controlThrow < -0.5f || Input.GetButtonDown("Jump")) {
                myPlayer.canMove = true;
                myDialogueManager.inDialogue = false;
                dollarGetText.SetActive(false);
                StartCoroutine("PutAwayDollar");
                dollarCutsceneState = 1;
            }
        }
    }

    IEnumerator StartIntermission() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Intermission");
    }

}
