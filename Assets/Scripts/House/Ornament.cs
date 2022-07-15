using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ornament : MonoBehaviour
{

    SpriteRenderer ornamentColor;
    float newR = 0f;
    float newG = 0f;
    float newB = 0f;
    Color lerpedColor;

    void Start() {
        ornamentColor = this.GetComponent<SpriteRenderer>();
        float randomInterval = Random.Range(0.75f, 1.5f);
        InvokeRepeating("ChangeColor", 0f, randomInterval);
    }

    void Update() {
        lerpedColor = Color.Lerp(ornamentColor.color, new Color(newR, newG, newB, 1f), 2f * Time.deltaTime);
        ornamentColor.color = lerpedColor;
    }

    void ChangeColor() {
        newR = Random.Range(0f, .9f);
        newG = Random.Range(0f, .9f);
        newB = Random.Range(0f, .9f);
    }
}
