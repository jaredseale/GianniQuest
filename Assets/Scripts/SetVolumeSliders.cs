using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumeSliders : MonoBehaviour
{
    [SerializeField] Slider masterSlider = null;
    [SerializeField] Slider musicSlider = null;
    [SerializeField] Slider SFXSlider = null;


    void Awake() {

        //if the values have been changed, get them, else set them at a default of max
        if (PlayerPrefs.HasKey("MusicSliderValue") && PlayerPrefs.HasKey("SFXSliderValue") && PlayerPrefs.HasKey("MasterVolumeSliderValue")) {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeSliderValue");
            musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue");
            SFXSlider.value = PlayerPrefs.GetFloat("SFXSliderValue");
        } else {
            PlayerPrefs.SetFloat("MasterVolumeSliderValue", 1f);
            PlayerPrefs.SetFloat("MusicSliderValue", 1f);
            PlayerPrefs.SetFloat("SFXSliderValue", 1f);
        }
    }

}
