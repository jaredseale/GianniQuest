using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    BoxCollider2D myCollider;
    [SerializeField] GameObject arrow;

    private void Start() {
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        ArrowBounce();
    }

    private void ArrowBounce() {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            arrow.SetActive(true);
        } else {
            arrow.SetActive(false);
        }
    }
}
