using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntranceDelay : MonoBehaviour
{

    [SerializeField] GameObject music;
    [SerializeField] GameObject boss;
    [SerializeField] float startDelay;

    void Start() {
        StartCoroutine(DelayedStart());
        
    }

    IEnumerator DelayedStart() {
        yield return new WaitForSeconds(startDelay);
        music.SetActive(true);
        boss.SetActive(true);
    }

}
