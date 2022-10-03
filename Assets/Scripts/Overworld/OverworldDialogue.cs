using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverworldDialogue : MonoBehaviour
{

    private Queue<string> sentences;

    public TextMeshProUGUI mainText;

    public Animator animator;
    AudioSource audioSource;
    public AudioClip textScrollSFX;
    public float speakerPitch = 1f;
    public bool actionTrigger = false;
    [SerializeField] bool currentlyTyping = false; //this variable allows the audio to stop playing when the text box is clicked through
    [SerializeField] GameObject dialogueBox;
    public bool inDialogue;
    [SerializeField] string currentWaypoint;
    [SerializeField] OverworldPlayer player;

    [SerializeField] int sentenceQueueLength;

    void Start() {
        sentences = new Queue<string>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        sentenceQueueLength = sentences.Count;

        if (inDialogue && Input.GetMouseButtonDown(0)) {
            SkipScroll();
        }

        if (Input.GetButtonDown("Jump") && !inDialogue && sentences.Count > 0) {
            StartDialogue();
        }

        currentWaypoint = player.currentWaypoint.gameObject.name;
        GetCurrentScript();
    }

    public void StartDialogue() {
        inDialogue = true;
        player.canMove = false;
        dialogueBox.SetActive(true);
        animator.SetBool("isOpen", true);
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
        foreach (char letter in sentence.ToCharArray()) {
            if (!currentlyTyping) {
                break;
            }
            audioSource.pitch = speakerPitch; // + Random.Range(-0.1f, 0.1f);
            audioSource.PlayOneShot(textScrollSFX);
            mainText.text += letter;
            yield return new WaitForSeconds(0.03f);

            if (letter == '!' || letter == '.' || letter == ',' || letter == '?') {
                yield return new WaitForSeconds(0.2f);
            }

        }
        currentlyTyping = false;
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        player.canMove = true;
        actionTrigger = true;
        inDialogue = false;
        StartCoroutine(WaitForDialogueBoxToScrollOffscreen());
    }

    IEnumerator WaitForDialogueBoxToScrollOffscreen() {
        yield return new WaitForSeconds(1f);
        sentences.Clear();
        dialogueBox.SetActive(false);
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

    void GetCurrentScript() {

        if (sentences.Count <= 1) {
            sentences.Clear();
            switch (currentWaypoint) {

                case "N4": //Ethereal Ascent
                    if (PlayerPrefs.GetString("EtherealAscentEntry") == "Closed") {
                        sentences.Enqueue("A mystical barrier prevents you from entering this area. Upon closer inspection, it's just a blue, locked gate.");
                    }
                    break;

                case "T": //The Sewers
                    if (PlayerPrefs.GetString("SewersEntry") == "Closed") {
                        sentences.Enqueue("The rusty manhole cover is closed with a lock. Despite your best efforts, you can't seem to chew through it. Perhaps a rusty key of some sort might be what you need.");
                    } else if (PlayerPrefs.GetString("SewersEntry") == "Done") {
                        sentences.Enqueue("You get the overwhelming sensation that you've explored everything here.");
                    }
                    break;

                case "J": //Rancid Rick's
                    if (PlayerPrefs.GetString("RicksEntry") == "Closed") {
                        sentences.Enqueue("It looks closed. There's a sign on the door that reads \"OPEN AT NIGHT\".");
                    }
                    break;

                case "A": //LCP
                    if (PlayerPrefs.GetString("LCPEntry") == "Done") {
                        sentences.Enqueue("There are a bunch of government type folks investigating the crashed spaceship. Probably best to keep your distance.");
                    }
                    break;

                case "V": //School
                    if (PlayerPrefs.GetString("SchoolEntry") == "Done") {
                        sentences.Enqueue("Mrs. Crimpt'n must have locked the doors on her way out.");
                    }
                    break;

                case "W": //SNICO
                    if (PlayerPrefs.GetString("SNICOEntry") == "Done") {
                        sentences.Enqueue("Nothing left to do here since you finished your shift.");
                    }
                    break;

                default:
                    sentences.Clear();
                    break;
            }
        }
    }

}
