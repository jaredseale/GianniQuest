using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleController : MonoBehaviour
{

    Animator myAnimator;
    void Start() {

        myAnimator = GetComponent<Animator>();
        InvokeRepeating("DesyncSparkle", 2f, 3f);
    }

    public void DesyncSparkle() {
        myAnimator.speed = Random.Range(0.5f, 1.5f);
    }
}
