using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrinkingGameManager : MonoBehaviour
{

    public GameObject soySauceBottle;
    public GameObject vodkaBottle;
    public GameObject shotGlass;

    [Space(20)]

    [SerializeField] SpriteRenderer handSprite;
    [SerializeField] Sprite handSpriteEmpty;
    [SerializeField] Sprite handSpriteBottle;
    [SerializeField] Sprite handSpriteShotGlass;
    public GameObject heldSoySauce;
    public GameObject heldVodka;
    public GameObject heldShotGlass;

    [Space(20)]

    [SerializeField] TextMeshProUGUI lairryScoreText;
    [SerializeField] int lairryScore;
    [SerializeField] TextMeshProUGUI gianniScoreText;
    [SerializeField] int gianniScore;
    [SerializeField] TextMeshProUGUI tryNumberText;
    [SerializeField] int tryNumber;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] int timer;

    [Space(20)]

    public string selectedObject;

    void Start() {
        FindObjectOfType<Pause>().canPause = false;
        selectedObject = "none";

        lairryScore = 0;
        lairryScoreText.text = lairryScore.ToString();
        gianniScore = 0;
        gianniScoreText.text = gianniScore.ToString();

        PlayerPrefs.SetInt("DrinkingGameTries", PlayerPrefs.GetInt("DrinkingGameTries") + 1);
        tryNumber = PlayerPrefs.GetInt("DrinkingGameTries");
        tryNumberText.text = AddOrdinal(tryNumber);
    }

    void Update() {
        if (selectedObject == "none") {
            handSprite.sprite = handSpriteEmpty;
        } else if (selectedObject == "Soy Sauce" || selectedObject == "Vodka") {
            handSprite.sprite = handSpriteBottle;
        } else if (selectedObject == "Shot Glass") {
            handSprite.sprite = handSpriteShotGlass;
        }
    }

    public void IncreaseGianniScore() {
        gianniScore += 1;
        gianniScoreText.text = gianniScore.ToString();
    }

    public static string AddOrdinal(int num) {
        if (num <= 0)
            return num.ToString();

        switch (num % 100) {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch (num % 10) {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }
    }
}
