using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLight : MonoBehaviour
{

    void Start() {
        if (PlayerPrefs.GetInt("BrokeGreenEgg") == 1) {
            Destroy(gameObject);        
        }
    }

    void Update() {
	
        
    }
}
