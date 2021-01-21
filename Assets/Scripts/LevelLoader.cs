using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] float loadSceneDelay = 2f;
    [SerializeField] AudioMixer mixer;
    [SerializeField] float musicVolumeToFadeTo;


    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithDelay(string sceneName) {
        StartCoroutine("Delay", sceneName);
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", loadSceneDelay, musicVolumeToFadeTo));
    }

    IEnumerator Delay(string sceneName) {
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(sceneName);
    }
}
