using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{

    BoxCollider2D myCollider;
    DrinkingGameManager myDrinkingGameManager;
    ShotGlass myShotGlass;
    [SerializeField] AudioSource gameSFX;
    [SerializeField] AudioClip buzzer;

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
        myDrinkingGameManager = FindObjectOfType<DrinkingGameManager>();
        myShotGlass = FindObjectOfType<ShotGlass>();
    }

    void Update() {
	    
        
    }

    void OnMouseUpAsButton() {
        if (myDrinkingGameManager.selectedObject == "none") {
            if (this.gameObject.name == "Soy Sauce") {
                if (myShotGlass.shotGlassState == "full" || myShotGlass.shotGlassState == "Soy Sauce") {
                    gameSFX.PlayOneShot(buzzer);
                    return;
                }

                myDrinkingGameManager.heldSoySauce.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            } else if (this.gameObject.name == "Vodka") {
                if (myShotGlass.shotGlassState == "full" || myShotGlass.shotGlassState == "Vodka") {
                    gameSFX.PlayOneShot(buzzer);
                    return;
                }

                myDrinkingGameManager.heldVodka.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

            myDrinkingGameManager.selectedObject = this.gameObject.name;
        }
    }
}
