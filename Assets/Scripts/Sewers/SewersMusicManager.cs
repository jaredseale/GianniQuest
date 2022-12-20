using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersMusicManager : MonoBehaviour
{

    public AudioSource fullTrack;
    public AudioSource loopTrack;
    public bool destroyable;

    private void Awake() { //when the scene is loaded, check if there is already a sewers music player coming in, and delete this one if so
        destroyable = false;
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
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

}
