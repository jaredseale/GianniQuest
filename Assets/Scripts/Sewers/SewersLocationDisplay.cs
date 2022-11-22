using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersLocationDisplay : MonoBehaviour
{

    void Awake() {
        if (PlayerPrefs.GetInt("SewersLocationDisplay") == 0) {
            this.gameObject.SetActive(false);
        }
        
    }

}
