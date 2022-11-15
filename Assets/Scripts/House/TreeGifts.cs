using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGifts : MonoBehaviour
{

    void Start() {
        if (PlayerPrefs.GetString("SantaDialogueState") != "PostDollar") {
            gameObject.SetActive(false);
        }
        
    }
}
