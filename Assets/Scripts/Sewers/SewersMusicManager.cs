using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersMusicManager : MonoBehaviour
{

    public AudioSource fullTrack;
    public AudioSource loopTrack;
    public bool destroyable;

    public int beat = 0;

    private void Awake() { //when the scene is loaded, check if there is already a sewers music player coming in, and delete this one if so
        destroyable = false;
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
        StartCoroutine(BeatTracker());
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (fullTrack.isPlaying == false) {
            fullTrack.gameObject.SetActive(false);
            loopTrack.gameObject.SetActive(true);
        }

        if (destroyable) {
            Destroy(gameObject);
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
