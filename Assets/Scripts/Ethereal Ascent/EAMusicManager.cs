using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAMusicManager : MonoBehaviour
{

    [SerializeField] AudioSource track2;
    [SerializeField] AudioSource track3;
    [SerializeField] AudioSource track4;
    [SerializeField] AudioSource track5;
    [SerializeField] AudioSource track6;

    float track2Threshold = 21f; //these will obviously need to be adjusted as the level is developed
    float track3Threshold = 55f;
    float track4Threshold = 91f;
    float track5Threshold = 129f;
    float track6Threshold = 200f;

    float fadeTimer = 4f;

    Player myPlayer;

    void Start() {
        myPlayer = FindObjectOfType<Player>();

        track2.volume = 0f;
        track3.volume = 0f;
        track4.volume = 0f;
        track5.volume = 0f;
        track6.volume = 0f;
    }

    void Update() {
        if (FindObjectOfType<EAManager>().itemCount < 20) {
            FadeInMusic();
            FadeOutMusic();
        }
    }

    void FadeInMusic() {

        if (track2.volume <= .001f && myPlayer.transform.position.y > track2Threshold) {
            StartCoroutine(FadeInTrack(track2));
        }

        if (track3.volume <= .001f && myPlayer.transform.position.y > track3Threshold) {
            StartCoroutine(FadeInTrack(track3));
        }

        if (track4.volume <= .001f && myPlayer.transform.position.y > track4Threshold) {
            StartCoroutine(FadeInTrack(track4));
        }

        if (track5.volume <= .001f && myPlayer.transform.position.y > track5Threshold) {
            StartCoroutine(FadeInTrack(track5));
        }

        if (track6.volume <= .001f && myPlayer.transform.position.y > track6Threshold) {
            StartCoroutine(FadeInTrack(track6));
        } 

    }

    void FadeOutMusic() {

        if (track2.volume >= .999f && myPlayer.transform.position.y < track2Threshold) {
            StartCoroutine(FadeOutTrack(track2));
        }

        if (track3.volume >= .999f && myPlayer.transform.position.y < track3Threshold) {
            StartCoroutine(FadeOutTrack(track3));
        }

        if (track4.volume >= .999f && myPlayer.transform.position.y < track4Threshold) {
            StartCoroutine(FadeOutTrack(track4));
        }

        if (track5.volume >= .999f && myPlayer.transform.position.y < track5Threshold) {
            StartCoroutine(FadeOutTrack(track5));
        }

        if (track6.volume >= .999f && myPlayer.transform.position.y < track6Threshold) {
            StartCoroutine(FadeOutTrack(track6));
        } 

    }

    IEnumerator FadeInTrack(AudioSource track) {
        float currentTime = 0;
        float currentVol = track.volume;
        float targetValue = 0.8f;

        while (currentTime < fadeTimer) {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / fadeTimer);
            track.volume = newVol;
            yield return null;
        }
        yield break;
    }

    IEnumerator FadeOutTrack(AudioSource track) {
        float currentTime = 0;
        float currentVol = track.volume;
        float targetValue = 0f;

        while (currentTime < fadeTimer) {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / fadeTimer);
            track.volume = newVol;
            yield return null;
        }
        yield break;
    }

    public void FadeInAll() {
        StartCoroutine(FadeInTrack(track2));
        StartCoroutine(FadeInTrack(track3));
        StartCoroutine(FadeInTrack(track4));
        StartCoroutine(FadeInTrack(track5));
        StartCoroutine(FadeInTrack(track6));
    }
}
