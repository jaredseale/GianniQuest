using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersMusicManager : MonoBehaviour
{

    public AudioSource fullTrack;
    public AudioSource loopTrack;

    void Start() {
	    
        
    }

    void Update() {
        if (fullTrack.isPlaying == false) {
            fullTrack.gameObject.SetActive(false);
            loopTrack.gameObject.SetActive(true);
        }
        
    }
}
