using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RancidRicksAnim : MonoBehaviour
{

    Animator myAnim;

    void Start() {
        myAnim = GetComponent<Animator>();
        if (PlayerPrefs.GetString("RicksEntry") == "Open") {
            myAnim.SetTrigger("stinkLinesAppear");
        }
    }

}
