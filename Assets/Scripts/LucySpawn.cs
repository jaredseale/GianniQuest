using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucySpawn : MonoBehaviour
{
    [SerializeField] Vector2[] spawnPositions;

    void Start() {
        Vector2 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)];


        if (spawnPos.y > -2f) { //if lucy spawns higher than the floor, make her appear behind gianni
            GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        }

        gameObject.transform.position = spawnPos;
    }

}
