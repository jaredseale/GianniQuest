using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class DrinkingGameManager : MonoBehaviour
{

    public GameObject soySauceBottle;
    public GameObject vodkaBottle;
    public GameObject shotGlass;
    public GameObject gameCamera;

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
    ShotGlass myShotGlass;

    [Space(20)]

    [SerializeField] int gameState1;
    [SerializeField] int gameState2;
    [SerializeField] int gameState3;
    [SerializeField] int gameState4;
    bool drinksMoving;
    [SerializeField] int cameraZoomSpeed;

    [Space(20)]

    [SerializeField] VideoPlayer myVideoPlayer;
    [SerializeField] GameObject rawImage;
    [SerializeField] VideoClip readyGoVideo;
    [SerializeField] VideoClip failureVideo;
    [SerializeField] VideoClip completeVideo;

    [Space(20)]

    [SerializeField] LevelLoader myLevelLoader;
    [SerializeField] Animator sceneTransition;

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
        myShotGlass = FindObjectOfType<ShotGlass>();

        drinksMoving = false;
    }

    void Update() {
        if (selectedObject == "none") {
            handSprite.sprite = handSpriteEmpty;
        } else if (selectedObject == "Soy Sauce" || selectedObject == "Vodka") {
            handSprite.sprite = handSpriteBottle;
        } else if (selectedObject == "Shot Glass") {
            handSprite.sprite = handSpriteShotGlass;
        }

        GameStateManager();
    }

    private void GameStateManager() {
        if (gianniScore < 3) {
            MoveTutorialArrow();
        } else {
            tutorialArrow.SetActive(false);
        }

        if (gianniScore >= gameState1) {
            myShotGlass.randomizeDrinkButtonPos = true;
        }

        if (gianniScore >= gameState2) {
            drinksMoving = true;
        }

        if (gianniScore >= gameState3) {
            gameCamera.GetComponent<Animator>().SetTrigger("beginPanning");
        }

        if (gianniScore >= gameState4) {
            float time = Mathf.PingPong(Time.time * cameraZoomSpeed, 1);
            gameCamera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(5f, 4f, time);
        }


    }

    private void MoveTutorialArrow() {

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

        if (drinksMoving == true) {
            soySauceBottle.GetComponent<DrinkBounce>().SetMoveSpeed(gianniScore);
            vodkaBottle.GetComponent<DrinkBounce>().SetMoveSpeed(gianniScore);
            shotGlass.GetComponent<DrinkBounce>().SetMoveSpeed(gianniScore);
        }
    }

    IEnumerator IncreaseLairryScore() {
        while (inGame && timer > 4) {
            yield return new WaitForSeconds(Random.Range(3f, 3.8f));
            lairryScore += 1;
            lairryScoreText.text = lairryScore.ToString();
        }
    }

    public void QueueGame() {
        tutorialWindow.SetActive(false);
        StartCoroutine("BeginGame");
    }

    IEnumerator BeginGame() {
        myVideoPlayer.clip = readyGoVideo;
        myVideoPlayer.Play();
        yield return new WaitForSeconds(0.2f);
        rawImage.SetActive(true);

        yield return new WaitForSeconds(2f);

        rawImage.SetActive(false);
        inGame = true;
        tutorialArrow.SetActive(true);
        tutorialState = 1;
        StartCoroutine("GameClock");
        StartCoroutine("IncreaseLairryScore");
    }

    IEnumerator EndGame() {
        if (gianniScore > lairryScore) {
            PlayerPrefs.SetString("LairryDialogueState", "WonContest");
            myVideoPlayer.clip = completeVideo;
            rawImage.SetActive(true);
            myVideoPlayer.Play();

            yield return new WaitForSeconds(2f);

        } else {
            PlayerPrefs.SetString("LairryDialogueState", "LostContest");
            myVideoPlayer.clip = failureVideo;
            rawImage.SetActive(true);
            myVideoPlayer.Play();

            yield return new WaitForSeconds(2f);
        }

        FindObjectOfType<SpawnPosition>().spawnPosition = new Vector2(-0.33f, -3.66f);
        sceneTransition.SetTrigger("doorTransition");
        myLevelLoader.loadSceneDelay = 2f;
        myLevelLoader.LoadSceneWithDelay("Rancid Rick's", false);
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
        StartCoroutine("EndGame");
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
