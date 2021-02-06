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
    [SerializeField] bool currentlyTyping = false; //this variable allows the audio to stop playing when the text box is clicked through
    [SerializeField] GameObject dialogueBox;

    void Start() {
        sentences = new Queue<string>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            currentlyTyping = false;
        }
    }

    public void StartDialogue(Dialogue dialogue) {
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

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        currentlyTyping = true;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        mainText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            if (!currentlyTyping) {
                break;
            }
            audioSource.pitch = speakerPitch + Random.Range(-0.1f, 0.1f);
            audioSource.PlayOneShot(textScrollSFX);
            mainText.text += letter;
            yield return new WaitForSeconds(0.03f);

        }
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        FindObjectOfType<Player>().canMove = true;
        StartCoroutine(WaitForDialogueBoxToScrollOffscreen());
    }

    IEnumerator WaitForDialogueBoxToScrollOffscreen() {
        yield return new WaitForSeconds(2f);
        dialogueBox.SetActive(false);
    }

}
