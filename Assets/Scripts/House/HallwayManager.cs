using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayManager : MonoBehaviour
{
    [SerializeField] GameObject tomGun;

    void Update() {
        if (PlayerPrefs.GetInt("HasGun") == 1) {
            tomGun.SetActive(false);
        }
        
    }
}
