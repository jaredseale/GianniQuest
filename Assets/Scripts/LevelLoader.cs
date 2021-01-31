using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] float loadSceneDelay = 2f;
    [SerializeField] float musicVolumeToFadeTo;
    [SerializeField] GameObject musicPlayer;


    public void Awake() {
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }


    public void LoadScene(string sceneName, bool musicFadeOut) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithDelay(string sceneName, bool musicFadeOut) {
        StartCoroutine("Delay", sceneName);

        if (musicFadeOut) { //this keeps the music going between scenes that use the same song
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", loadSceneDelay, musicVolumeToFadeTo));
        } else {
            DontDestroyOnLoad(FindObjectOfType<MusicPlayer>());
        }
        
    }

    IEnumerator Delay(string sceneName) {
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(sceneName);
    }
}
