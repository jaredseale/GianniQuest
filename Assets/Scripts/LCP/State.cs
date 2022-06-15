using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{

    [TextArea(4, 10)] public string dialogueText;
    [TextArea(1, 1)] public string characterName;
    public Sprite characterSprite;
    public AudioClip characterVoice;
    public Sprite backgroundSprite;
    public AudioClip backgroundMusic;
    public AudioClip SFX;
    public string[] choices;
    public string executeMethodAfterDialogue;
    public State[] nextStates;
}
