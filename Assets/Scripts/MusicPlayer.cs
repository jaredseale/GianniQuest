using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private void Awake() { //when the scene is loaded, check if there is already a music player coming in, and delete this one if so
        if (FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);
        }
    }
}
