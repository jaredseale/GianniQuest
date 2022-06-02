using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

    [SerializeField] GameObject rootMenu;
    [SerializeField] GameObject burgerMenu;
    [SerializeField] GameObject hotDogMenu;
    [SerializeField] GameObject friesMenu;
    [SerializeField] GameObject drinksMenu;

    void Start() {
	
        
    }

    void Update() {
	
        
    }

    public void ShowBurgerMenu() {
        burgerMenu.SetActive(true);
        rootMenu.SetActive(false);
    }

    public void BackFromBurgerMenu() {
        rootMenu.SetActive(true);
        burgerMenu.SetActive(false);
    }

    public void ShowHotDogMenu() {
        hotDogMenu.SetActive(true);
        rootMenu.SetActive(false);
    }

    public void BackFromHotDogMenu() {
        rootMenu.SetActive(true);
        hotDogMenu.SetActive(false);
    }

    public void ShowFriesMenu() {
        friesMenu.SetActive(true);
        rootMenu.SetActive(false);
    }
    public void BackFromFriesMenu() {
        rootMenu.SetActive(true);
        friesMenu.SetActive(false);
    }

    public void ShowDrinksMenu() {
        drinksMenu.SetActive(true);
        rootMenu.SetActive(false);
    }
    public void BackFromDrinksMenu() {
        rootMenu.SetActive(true);
        drinksMenu.SetActive(false);
    }

}
