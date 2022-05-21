using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCPStatus : MonoBehaviour
{
    [SerializeField] GameObject LCPNormal;
    [SerializeField] GameObject LCPCrashed;

    void Start() {
        if (PlayerPrefs.GetString("LCPSpriteState") == "Normal") {
            LCPCrashed.SetActive(false);
        } else {
            LCPNormal.SetActive(false);
        }
        
    }

}
