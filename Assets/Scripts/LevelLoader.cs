using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public float loadSceneDelay = 2f;
    [SerializeField] float musicVolumeToFadeTo;
    [SerializeField] GameObject musicPlayer;
    bool destroyMusicPlayer;

    public void Awake() {
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }


    //public void LoadScene(string sceneName, bool musicFadeOut) {
    //    SceneManager.LoadScene(sceneName);
    //}

    public void LoadSceneWithDelay(string sceneName, bool musicFadeOut) {
        if (FindObjectOfType<Pause>()) {
            FindObjectOfType<Pause>().canPause = false;
        }
        destroyMusicPlayer = musicFadeOut;
        StartCoroutine("Delay", sceneName);

        if (musicFadeOut) { //this keeps the music going between scenes that use the same song
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", loadSceneDelay, musicVolumeToFadeTo));
        } else {
            DontDestroyOnLoad(FindObjectOfType<MusicPlayer>());
        }
        
    }

    IEnumerator Delay(string sceneName) {
        yield return new WaitForSeconds(loadSceneDelay);

        if (destroyMusicPlayer) {
            FindObjectOfType<MusicPlayer>().destroyable = true;
        }
        yield return new WaitForSeconds(0.1f); //this lets the music player destroy itself before the next scene is loaded

        SceneManager.LoadScene(sceneName);
    }
}
