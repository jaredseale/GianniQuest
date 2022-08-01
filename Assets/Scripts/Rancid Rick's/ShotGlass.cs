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
    [SerializeField] AudioSource gameSFX;
    [SerializeField] AudioClip buzzer;
    [SerializeField] AudioClip glug;
    [SerializeField] AudioClip drinkPour;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        myDrinkingGameManager = FindObjectOfType<DrinkingGameManager>();
        shotGlassState = "empty";
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

        drinkButton.SetActive(false);
    }

    void OnMouseUpAsButton() {
        
        myDrinkingGameManager.heldVodka.SetActive(false);
        myDrinkingGameManager.heldSoySauce.SetActive(false);

        if (myDrinkingGameManager.selectedObject == "none" && shotGlassState == "full") {
            myDrinkingGameManager.selectedObject = this.gameObject.name;
            myDrinkingGameManager.shotGlass.GetComponent<SpriteRenderer>().enabled = false;
            myDrinkingGameManager.heldShotGlass.SetActive(true);
            shotGlassSprite.sprite = shotGlassEmpty;

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
        }
    }
}
