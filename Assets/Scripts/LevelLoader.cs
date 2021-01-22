using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] float loadSceneDelay = 2f;
    [SerializeField] float musicVolumeToFadeTo;


    public void Awake() {
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

    public void LoadScene(string sceneName, bool fadeOut) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithDelay(string sceneName, bool fadeOut) {
        StartCoroutine("Delay", sceneName);

        if (fadeOut) {
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", loadSceneDelay, musicVolumeToFadeTo));
        }
        
    }

    IEnumerator Delay(string sceneName) {
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(sceneName);
    }
}
