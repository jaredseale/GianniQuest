using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public bool destroyable;

    private void Awake() { //when the scene is loaded, check if there is already a music player coming in, and delete this one if so
        destroyable = false;
        if (FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (destroyable) {
            Destroy(gameObject);
        }
    }
}
