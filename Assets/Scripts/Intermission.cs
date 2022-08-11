using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intermission : MonoBehaviour
{

    float prevVolume;

    void Start() {
        prevVolume = FindObjectOfType<AudioSource>().volume;
        FindObjectOfType<SpawnPosition>().setNextSpawn(-6.26f, -0.16f);
        FindObjectOfType<AudioSource>().volume = 0f;
        PlayerPrefs.SetString("TimeOfDay", "Night");
        PlayerPrefs.SetString("SisterDialogueState", "Night");
        PlayerPrefs.SetString("BrotherDialogueState", "Night");
        PlayerPrefs.SetString("MomDialogueState", "Night");
        PlayerPrefs.SetString("DadDialogueState", "Night");
        PlayerPrefs.SetString("RicksEntry", "Open");

        StartCoroutine("Delay");
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(16f);
        FindObjectOfType<AudioSource>().volume = prevVolume;
        SceneManager.LoadScene("Gianni's Room");
    }

}
