using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{

    [SerializeField] float spawnDelay;
    [SerializeField] GameObject leftToRightUFO;
    [SerializeField] GameObject rightToLeftUFO;
    [SerializeField] bool rightToLeft;

    void Start() {
        StartCoroutine(SpawnUFOLoop());
    }


    IEnumerator SpawnUFOLoop() {
        while (true) {
            yield return new WaitForSeconds(spawnDelay);
            if (rightToLeft) {
                Instantiate(rightToLeftUFO, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            } else {
                Instantiate(leftToRightUFO, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            }
        }
    }

}
