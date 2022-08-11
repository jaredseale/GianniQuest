using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI mainText;

    public Animator animator;
    AudioSource audioSource;
    public AudioClip textScrollSFX;
    public float speakerPitch = 1f;
    public bool actionTrigger = false;
    [SerializeField] bool currentlyTyping = false; //this variable allows the audio to stop playing when the text box is clicked through
    [SerializeField] GameObject dialogueBox;
    public GameObject dialogueTarget;
    public bool inDialogue;
    Pause myPause;

    void Start() {
        myPause = FindObjectOfType<Pause>();
        sentences = new Queue<string>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (inDialogue && Input.GetMouseButtonDown(0)) {
            SkipScroll();
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        myPause.canPause = false;
        inDialogue = true;
        dialogueBox.SetActive(true);
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Peek();
        StopAllCoroutines();
        currentlyTyping = true;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        mainText.text = "";
        float textSpeed = dialogueTarget.GetComponent<NPC>().textSpeed;
        foreach (char letter in sentence.ToCharArray()) {
            if (!currentlyTyping) {
                break;
            }

            audioSource.pitch = speakerPitch + Random.Range(-0.1f, 0.1f);
            audioSource.PlayOneShot(dialogueTarget.GetComponent<NPC>().voiceSFX);
            mainText.text += letter;
            yield return new WaitForSeconds(0.03f / textSpeed);

            if (letter == '!' || letter == '.' || letter == ',' || letter == '?') {
                yield return new WaitForSeconds(0.2f / textSpeed);
            }
        }
        currentlyTyping = false;
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        FindObjectOfType<Player>().canMove = true;
        actionTrigger = true;
        inDialogue = false;
        myPause.canPause = true;
        StartCoroutine(WaitForDialogueBoxToScrollOffscreen());
    }

    IEnumerator WaitForDialogueBoxToScrollOffscreen() {
        yield return new WaitForSeconds(.05f);
        actionTrigger = false; //this is a bit hacky but it fixes a bug where talking to NPCs will let the action trigger remain active
        yield return new WaitForSeconds(0.95f);
        dialogueBox.SetActive(false);
        dialogueTarget = null;
    }

    void SkipScroll() {
        if (!currentlyTyping) {
            sentences.Dequeue();
            DisplayNextSentence();
        } else {
            StopAllCoroutines();
            mainText.text = sentences.Peek();
            currentlyTyping = false;
        }


    }

}
