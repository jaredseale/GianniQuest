using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    [SerializeField] GameObject daySkySprite;
    [SerializeField] GameObject nightSkySprite;

    private void Update() { //set the outside of windows to reflect the state of the game
        if (PlayerPrefs.GetString("TimeOfDay") == "Day") {
            daySkySprite.SetActive(true);
            nightSkySprite.SetActive(false);
        } else if (PlayerPrefs.GetString("TimeOfDay") == "Night") {
            daySkySprite.SetActive(false);
            nightSkySprite.SetActive(true);
        }
    }
}
