using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public Vector2 spawnPosition;

    private void Awake() { //when the scene is loaded, check if there is already a spawn manager coming in, and delete this one if so
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void setNextSpawn(float spawnXPos, float spawnYPos) {
        spawnPosition = new Vector2(spawnXPos, spawnYPos);
    }
}
