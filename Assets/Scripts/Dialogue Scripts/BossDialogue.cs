using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogue : MonoBehaviour
{

    [SerializeField] GameObject bliss;
    [SerializeField] GameObject crossfade;

    public DialogueTrigger currentDialogue;
    public DialogueManager dialogueManager;
    float musicVolume;
    float musicSliderValue;
    Player player;

    public string currentState;

    string[] Init = new string[] {"Cease! Cease! I beg of thee for a ceasefire!",
        "Alas, thou hast thrust the bitter taste of defeat upon my beak and I no longer have the ability to...",
        "*ahem* I say, ALAS, THOU HAST THRUST THE BITTER TASTE OF DEFEAT UPON... gracious... that foul music hath assaulted mine ears for ages now...",
        "Couldst thou, perchance, lessen the intensity of the melodies that echo in this chamber?" };
    string[] Init2 = new string[] { "I said can you turn down the music dude?" };
    string[] MusicDown = new string[] {"Ahhh... at last, I am able to collect my thoughts in peace!",
        "My deepest apologies for my aggressive behaviour upon meeting thou in these accursed waterways, I simply mistook thou as a threat to myself and my children.",
        "However, upon reflection, I have come to realize thou art the individual who hath freed them from their ovoid prisons!",
        "My family was exiled to this faraway land after the lords of my kingdom banished us under circumstances involving, say, tax fraud and money laundering. And grand conspiracy. Arson, as well.",
        "I regrettably must live out the rest of my days trapped within these walls.",
        "Ahhh... if one could only show me even a glimpse of the outside world again, I would reward one handsomely."};
    string[] MusicDown2 = new string[] { "Ahhh... if one could only show me even a glimpse of the outside world again, I would reward one handsomely indeed." };
    string[] BlissUp = new string[] { "Heavens! 'Tis the most breathtaking scene I hath ever lain my eyes upon!",
        "Certainly you have... I mean thou hath... earned the spoils of my treason! Behold!" };
    string[] End = new string[] { "My deepest gratitude for the reunification of my family. Allow me to assist thou out of this underworld. I shall also restore thine wicked tunes." };

    private void Start() {
        player = FindObjectOfType<Player>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        currentState = "Init";
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicSliderValue = PlayerPrefs.GetFloat("MusicSliderValue");
        SetDialogue();
    }

    private void Update() {

        if (dialogueManager.dialogueTarget == this.gameObject) {
            if (dialogueManager.actionTrigger == true) {
                dialogueManager.actionTrigger = false;
                PlayAction();
            }
        }

        if (PlayerPrefs.GetFloat("MusicVolume") < -10f) {
            if (currentState == "Init2") {
                currentState = "MusicDown";
            }
        }

        if (bliss.activeInHierarchy == true && currentState == "MusicDown2") {
            currentState = "BlissUp";
        }

        SetDialogue();

    }

    private void SetDialogue() {
        if (currentState == "Init") {
            currentDialogue.dialogue.sentences = Init;
        } else if (currentState == "Init2") {
            currentDialogue.dialogue.sentences = Init2;
        } else if (currentState == "MusicDown") {
            currentDialogue.dialogue.sentences = MusicDown;
        } else if (currentState == "MusicDown2") {
            currentDialogue.dialogue.sentences = MusicDown2;
        } else if (currentState == "BlissUp") {
            currentDialogue.dialogue.sentences = BlissUp;
        } else if (currentState == "End") {
            currentDialogue.dialogue.sentences = End;
        }
    }

    void PlayAction() {

        if (currentState == "Init") {
            currentState = "Init2";
        } else if (currentState == "MusicDown") {
            currentState = "MusicDown2";
        } else if (currentState == "BlissUp") {
            PlayerPrefs.SetString("SewersEntry", "Done");
            PlayerPrefs.SetInt("SewersDataManagement", 0);
            Dollar myDollar = FindObjectOfType<Dollar>();
            myDollar.CollectDollar();
            currentState = "End";
            if (FindObjectsOfType<SpeedrunManager>() != null && FindObjectOfType<SpeedrunManager>().speedrunType == "sewers") {
                FindObjectOfType<SpeedrunManager>().runGoing = false;
                FindObjectOfType<SpeedrunManager>().destroyable = true;
            }
        } else if (currentState == "End") {
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("MusicSliderValue", musicSliderValue);
            FindObjectOfType<LevelLoader>().LoadSceneWithDelay("Overworld", true);
            crossfade.GetComponent<Animator>().SetTrigger("levelTransition");
            player.canMove = false;
            gameObject.SetActive(false);
        }
    }
}
