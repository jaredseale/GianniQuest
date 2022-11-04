using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform cam;
    [SerializeField] bool horizontalParallax;
    [SerializeField] bool verticalParallax;

    void Update() {

        if (verticalParallax) {
            transform.position = new Vector2(transform.position.x, 0.3f * cam.position.y);
        }
        
        if (horizontalParallax) {
            transform.position = new Vector2(0.3f * cam.position.x, 0.3f * transform.position.y);
        }
        
        
    }
}
