using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform cam;

    void Update() {
        transform.position = new Vector2(transform.position.x, 0.3f * cam.position.y);
        
    }
}
