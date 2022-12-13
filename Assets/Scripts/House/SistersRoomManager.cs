using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistersRoomManager : MonoBehaviour
{

    [SerializeField] GameObject keke;

    void Start() {
        if (PlayerPrefs.GetInt("Room11Wall") != 1) {
            Destroy(keke.gameObject);
        }

    }

}
