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
    public int tutorialState;
    [SerializeField] GameObject tutorialWindow;
    [SerializeField] GameObject tutorialArrow;
    Vector2 tutorialArrowPosition;
    Vector3 tutorialArrowRotation;
    public bool inGame;

    void Start() {
        inGame = false;
        FindObjectOfType<Pause>().canPause = false;
        selectedObject = "none";

        timer = 59;
        timerText.text = timer.ToString();
        lairryScore = 0;
        lairryScoreText.text = lairryScore.ToString();
        gianniScore = 0;
        gianniScoreText.text = gianniScore.ToString();

        tutorialState = 0;

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

        if (gianniScore < 3) {
            MoveTutorialArrow();
        } else {
            tutorialArrow.SetActive(false);
        }

    }

    private void MoveTutorialArrow() {
        tutorialArrow.SetActive(true);

        switch (tutorialState) {
            case 0:
                break;

            case 1:
                tutorialArrowPosition = new Vector2(-7.33f, 0f);
                tutorialArrowRotation = new Vector3(0f, 0f, 0f);
                break;

            case 2:
                tutorialArrowPosition = new Vector2(0f, 2.61f);
                tutorialArrowRotation = new Vector3(0f, 0f, -90f);
                break;

            case 3:
                tutorialArrowPosition = new Vector2(7.15f, 0.53f);
                tutorialArrowRotation = new Vector3(0f, 0f, 180f);
                break;

            case 4:
                tutorialArrowPosition = new Vector2(0f, 2.61f);
                tutorialArrowRotation = new Vector3(0f, 0f, -90f);
                break;

            case 5:
                tutorialArrowPosition = new Vector2(0f, -1.57f);
                tutorialArrowRotation = new Vector3(0f, 0f, 90f);
                break;

            case 6:
                tutorialArrowPosition = new Vector2(0f, -1.57f);
                tutorialArrowRotation = new Vector3(0f, 0f, -90f);
                break;

            default:
                Debug.Log("Erroneous state, fix that :(");
                break;
        }

        tutorialArrow.transform.position = tutorialArrowPosition;
        tutorialArrow.transform.rotation = Quaternion.Euler(tutorialArrowRotation);
    }

    public void IncreaseGianniScore() {
        gianniScore += 1;
        gianniScoreText.text = gianniScore.ToString();
    }

    public void BeginGame() {
        tutorialWindow.SetActive(false);
        //start animation coroutine, move the next lines to that when i have it
        inGame = true;
        tutorialState = 1;
        StartCoroutine("GameClock");
    }

    public void EndGame() {
        
    }

    IEnumerator GameClock() {
        while (timer >= 0) {
            if (timer < 10) {
                timerText.text = "0" + timer.ToString();
            } else {
                timerText.text = timer.ToString();
            }
            
            yield return new WaitForSecondsRealtime(1f);
            timer -= 1;
        }

        inGame = false;
        EndGame();
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
