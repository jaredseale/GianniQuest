using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{

    [TextArea(14, 10)][SerializeField] string textArea;
    [TextArea(14, 1)][SerializeField] string characterName;
    public Sprite characterSprite;
    public Sprite backgroundSprite;
    public AudioClip backgroundMusic;
    public AudioClip backgroundSFX;
    public AudioClip beginningSFX;
    public string[] choices;
    [SerializeField] State[] nextStates;

    void Start() {
	
        
    }

    void Update() {
	
        
    }
}
