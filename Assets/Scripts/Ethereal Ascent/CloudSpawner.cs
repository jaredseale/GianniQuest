using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject cloud;
    [SerializeField] float spawnDelay;
    int coinFlip;
    void Start() {
        StartCoroutine(SpawnCloudLoop());
    }

    IEnumerator SpawnCloudLoop() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(spawnDelay, spawnDelay + 5f));
            coinFlip = Random.Range(0, 7);
            if (coinFlip == 0) {
                Instantiate(cloud, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            }
            
        }
    }

}
