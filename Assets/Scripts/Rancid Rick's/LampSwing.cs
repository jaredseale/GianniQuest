using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwing : MonoBehaviour
{

    Animator myAnimator;
    void Start() {

        myAnimator = GetComponent<Animator>();
        InvokeRepeating("DesyncLamp", 0f, 5f);
    }

    public void DesyncLamp() {
        myAnimator.speed = Random.Range(0.8f, 1.2f);
    }
}
