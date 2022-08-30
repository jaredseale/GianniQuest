using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject cloud;
    [SerializeField] float spawnDelay;
    [SerializeField] int range;

    int coinFlip;
    void Start() {
        StartCoroutine(SpawnCloudLoop());
    }

    IEnumerator SpawnCloudLoop() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(spawnDelay, spawnDelay + 5f));
                range = 7;
            if (gameObject.transform.position.y > 128f) {
                range = 15;
            }
            coinFlip = Random.Range(0, range);
            if (coinFlip == 0) {
                Instantiate(cloud, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            }
            
        }
    }

}
