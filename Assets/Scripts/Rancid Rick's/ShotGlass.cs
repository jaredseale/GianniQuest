using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGlass : MonoBehaviour
{
    [SerializeField] SpriteRenderer shotGlassSprite;
    [SerializeField] Sprite shotGlassEmpty;
    [SerializeField] Sprite shotGlassVodka;
    [SerializeField] Sprite shotGlassSoySauce;
    [SerializeField] Sprite shotGlassFull;

    [Space(20)]

    BoxCollider2D myCollider;
    DrinkingGameManager myDrinkingGameManager;
    public string shotGlassState;
    [SerializeField] GameObject drinkButton;
    public bool randomizeDrinkButtonPos;
    [SerializeField] AudioSource gameSFX;
    [SerializeField] AudioClip buzzer;
    [SerializeField] AudioClip glug;
    [SerializeField] AudioClip drinkPour;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        myDrinkingGameManager = FindObjectOfType<DrinkingGameManager>();
        shotGlassState = "empty";
        randomizeDrinkButtonPos = false;
    }

    void Update() {
    }

    public void DrinkSmirsauce() {

        myDrinkingGameManager.selectedObject = "none";
        myDrinkingGameManager.heldShotGlass.SetActive(false);
        myDrinkingGameManager.shotGlass.GetComponent<SpriteRenderer>().enabled = true;
        shotGlassSprite.sprite = shotGlassEmpty;
        shotGlassState = "empty";

        gameSFX.PlayOneShot(glug);
        myDrinkingGameManager.IncreaseGianniScore();
        myDrinkingGameManager.tutorialState = 1;

        drinkButton.SetActive(false);
    }

    void OnMouseDown() {

        if (myDrinkingGameManager.inGame == true) {
            myDrinkingGameManager.heldVodka.SetActive(false);
            myDrinkingGameManager.heldSoySauce.SetActive(false);

            if (myDrinkingGameManager.selectedObject == "none" && shotGlassState == "full") {
                myDrinkingGameManager.selectedObject = this.gameObject.name;
                myDrinkingGameManager.shotGlass.GetComponent<SpriteRenderer>().enabled = false;
                myDrinkingGameManager.heldShotGlass.SetActive(true);
                shotGlassSprite.sprite = shotGlassEmpty;

                if (randomizeDrinkButtonPos) {
                    SetDrinkButtonPosition();
                }

                drinkButton.SetActive(true);
            } else if (myDrinkingGameManager.selectedObject == "Vodka" && shotGlassState == "empty") {
                gameSFX.PlayOneShot(drinkPour);
                shotGlassSprite.sprite = shotGlassVodka;
                shotGlassState = "Vodka";
                myDrinkingGameManager.vodkaBottle.GetComponent<SpriteRenderer>().enabled = true;
                myDrinkingGameManager.selectedObject = "none";
            } else if (myDrinkingGameManager.selectedObject == "Soy Sauce" && shotGlassState == "empty") {
                gameSFX.PlayOneShot(drinkPour);
                shotGlassSprite.sprite = shotGlassSoySauce;
                shotGlassState = "Soy Sauce";
                myDrinkingGameManager.soySauceBottle.GetComponent<SpriteRenderer>().enabled = true;
                myDrinkingGameManager.selectedObject = "none";
            } else if (myDrinkingGameManager.selectedObject == "Vodka" && shotGlassState == "Soy Sauce") {
                gameSFX.PlayOneShot(drinkPour);
                shotGlassSprite.sprite = shotGlassFull;
                shotGlassState = "full";
                myDrinkingGameManager.vodkaBottle.GetComponent<SpriteRenderer>().enabled = true;
                myDrinkingGameManager.selectedObject = "none";
            } else if (myDrinkingGameManager.selectedObject == "Soy Sauce" && shotGlassState == "Vodka") {
                gameSFX.PlayOneShot(drinkPour);
                shotGlassSprite.sprite = shotGlassFull;
                shotGlassState = "full";
                myDrinkingGameManager.soySauceBottle.GetComponent<SpriteRenderer>().enabled = true;
                myDrinkingGameManager.selectedObject = "none";
            } else if (myDrinkingGameManager.selectedObject == "Shot Glass") {
                return;
            } else {
                gameSFX.PlayOneShot(buzzer);
                myDrinkingGameManager.selectedObject = "none";
                return;
            }

            myDrinkingGameManager.tutorialState += 1;
        }
    }

    private void SetDrinkButtonPosition() {

        float randX = Random.Range(-550f, 550f) * 0.01f; //the 0.01f is to compensate for the canvas scaling
        float randY = Random.Range(-350f, 350f) * 0.01f;

        int flipChance = Random.Range(1, 10); //10% chance to be backwards

        drinkButton.transform.position = new Vector2(randX, randY);

        if (flipChance == 1) {
            drinkButton.transform.localScale = new Vector3(-4f, 4, 4);
        } else {
            drinkButton.transform.localScale = new Vector3(4f, 4, 4);
        }

    }
}
