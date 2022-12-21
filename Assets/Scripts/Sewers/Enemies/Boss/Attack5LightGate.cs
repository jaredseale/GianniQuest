using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack5LightGate : MonoBehaviour
{

    [SerializeField] GameObject bird;
    AudioSource myAudio;


    public void SpawnBird() {
        bird.SetActive(true);
    }

    public void TurnOff() {
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        myAudio = GetComponent<AudioSource>();
        myAudio.Play();
    }
}
