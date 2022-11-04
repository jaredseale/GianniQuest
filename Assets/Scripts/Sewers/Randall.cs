using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randall : MonoBehaviour
{

    void Start() {
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

}
