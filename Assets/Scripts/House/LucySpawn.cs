using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucySpawn : MonoBehaviour
{
    [SerializeField] Vector2[] spawnPositions;

    void Start() {
        int spawnPosIndex = Random.Range(0, spawnPositions.Length);
        Vector2 spawnPos = spawnPositions[spawnPosIndex];

        if (spawnPos.y > -2.5f) { //if lucy spawns higher than the floor, make her appear behind gianni
            GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        }

        if (spawnPosIndex <= 2) {
            GetComponent<NPC>().enabled = true;
        }

        gameObject.transform.position = spawnPos;
    }

}
