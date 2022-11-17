using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersMusicManager : MonoBehaviour
{

    public AudioSource fullTrack;
    public AudioSource loopTrack;

    public int beat = 0;

    void Start() {
        StartCoroutine(BeatTracker());
    }

    void Update() {
        if (fullTrack.isPlaying == false) {
            fullTrack.gameObject.SetActive(false);
            loopTrack.gameObject.SetActive(true);
        }
        
    }

    IEnumerator BeatTracker() {
        while (true) {
            yield return new WaitForSecondsRealtime(5f);
            beat++;
            if (beat > 4) {
                beat = 1;
            }
        }
    }
}
