using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GianniClone : MonoBehaviour
{

    [SerializeField] BoxCollider2D triggerCollider;
    Rigidbody2D myRB;
    [SerializeField] BoxCollider2D bodyCollider;
    [SerializeField] BoxCollider2D footCollider;
    AudioSource myAudio;

    void Start() {
        myRB = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
        StartCoroutine(DelayedCollider());
    }

    private void Update() {
        if (footCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            myRB.constraints = RigidbodyConstraints2D.FreezeAll; //prevents the clone from falling through the ground if spawned while standing on ground
        }
    }

    IEnumerator DelayedCollider() {
        yield return new WaitForSeconds(0.05f);

        while (true) {
            if (!triggerCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
                bodyCollider.enabled = true;
                this.enabled = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }   

}
