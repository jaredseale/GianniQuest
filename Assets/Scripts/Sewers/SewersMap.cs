using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SewersMap : MonoBehaviour
{

    [SerializeField] GameObject gianniIcon;

    [SerializeField] GameObject greenEggMask;
    [SerializeField] GameObject redEggMask;
    [SerializeField] GameObject blueEggMask;
    [SerializeField] GameObject orangeEggMask;
    [SerializeField] GameObject purpleEggMask;
    [SerializeField] GameObject room9Mask;
    [SerializeField] GameObject room10Mask;
    [SerializeField] GameObject room11Mask;
    [SerializeField] GameObject room12Mask;
    [SerializeField] GameObject room13Mask;
    [SerializeField] GameObject room15Mask;
    [SerializeField] GameObject room16Mask;

    Vector2 room1Pos = new Vector2(14.7f, 0.5f);
    Vector2 room2Pos = new Vector2(-3.5f, -6.5f);
    Vector2 room3Pos = new Vector2(-18.1f, -6.6f);
    Vector2 room4Pos = new Vector2(-34.7f, -6.4f);
    Vector2 room5Pos = new Vector2(-34.4f, 6.2f);
    Vector2 room6Pos = new Vector2(-34.6f, 18.5f);
    Vector2 room7Pos = new Vector2(-12.3f, 2.1f);
    Vector2 room8Pos = new Vector2(32.7f, -13.8f);
    Vector2 room9Pos = new Vector2(-9.4f, 9.4f);
    Vector2 room10Pos = new Vector2(-3.6f, 18.4f);
    Vector2 room11Pos = new Vector2(48f, 0.3f);
    Vector2 room12Pos = new Vector2(-49.2f, 1.7f);
    Vector2 room13Pos = new Vector2(-18.6f, 18.5f);
    Vector2 room15Pos = new Vector2(-15.8f, -19f);
    Vector2 room16Pos = new Vector2(17.5f, -21.1f);



    void Start() {
        PlaceIcon();
    }

    void Update() {
        RevealMasks();
    }

    private void RevealMasks() {
        if (PlayerPrefs.GetInt("BrokeGreenEgg") == 1) {
            greenEggMask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("BrokeRedEgg") == 1) {
            redEggMask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("BrokeBlueEgg") == 1) {
            blueEggMask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("BrokeOrangeEgg") == 1) {
            orangeEggMask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("BrokePurpleEgg") == 1) {
            purpleEggMask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SMRoom9") == 1) {
            room9Mask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SMRoom10") == 1) {
            room10Mask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SMRoom11") == 1) {
            room11Mask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SMRoom12") == 1) {
            room12Mask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SMRoom1314") == 1) {
            room13Mask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SMRoom15") == 1) {
            room15Mask.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SMRoom16") == 1) {
            room16Mask.SetActive(false);
        }
    }

    private void PlaceIcon() {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName) {
            case "Sewers 1":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room1Pos;
                break;

            case "Sewers 2":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room2Pos;
                break;

            case "Sewers 3":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room3Pos;
                break;

            case "Sewers 4":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room4Pos;
                break;

            case "Sewers 5":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room5Pos;
                break;

            case "Sewers 6":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room6Pos;
                break;

            case "Sewers 7":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room7Pos;
                break;

            case "Sewers 8":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room8Pos;
                break;

            case "Sewers 9":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room9Pos;
                break;

            case "Sewers 10":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room10Pos;
                break;

            case "Sewers 11":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room11Pos;
                break;

            case "Sewers 12":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room12Pos;
                break;

            case "Sewers 13":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room13Pos;
                break;

            case "Sewers 14":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room13Pos;
                break;

            case "Sewers 15":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room15Pos;
                break;

            case "Sewers 16":
                gianniIcon.GetComponent<RectTransform>().anchoredPosition = room16Pos;
                break;

            default:
                Debug.Log("you either aren't in the sewers or you messed up somewhere");
                break;
        }
    }
}
