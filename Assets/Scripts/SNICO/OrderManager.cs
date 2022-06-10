using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public List<List<string>> currentOrder;

    [SerializeField] TextMeshProUGUI receiptText;

    [SerializeField] GameObject rootMenu;
    [SerializeField] GameObject burgerMenu;
    [SerializeField] GameObject hotDogMenu;
    [SerializeField] GameObject friesMenu;
    [SerializeField] GameObject drinksMenu;

    [SerializeField] GameObject burgerBunsButton;
    [SerializeField] GameObject burgerSinglePattyButton;
    [SerializeField] GameObject burgerDoublePattyButton;
    [SerializeField] GameObject burgerCheeseButton;
    [SerializeField] GameObject burgerLettuceButton;
    [SerializeField] GameObject burgerOnionsButton;
    [SerializeField] GameObject burgerPicklesButton;
    [SerializeField] GameObject burgerBaconButton;
    [SerializeField] GameObject burgerKetchupButton;
    [SerializeField] GameObject burgerMustardButton;
    [SerializeField] GameObject burgerMayoButton;

    [SerializeField] GameObject hotDogBunButton;
    [SerializeField] GameObject hotDogWienerButton;
    [SerializeField] GameObject hotDogLettuceButton;
    [SerializeField] GameObject hotDogSauerKrautButton;
    [SerializeField] GameObject hotDogChiliButton;
    [SerializeField] GameObject hotDogRelishButton;
    [SerializeField] GameObject hotDogKetchupButton;
    [SerializeField] GameObject hotDogMustardButton;
    [SerializeField] GameObject hotDogMayoButton;

    [SerializeField] GameObject friesSaltButton;
    [SerializeField] GameObject friesCheeseButton;
    [SerializeField] GameObject friesSmallButton;
    [SerializeField] GameObject friesMediumButton;
    [SerializeField] GameObject friesLargeButton;

    [SerializeField] GameObject drinksWorterButton;
    [SerializeField] GameObject drinksSprightButton;
    [SerializeField] GameObject drinksKongKolaButton;
    [SerializeField] GameObject drinksDrCardboardButton;
    [SerializeField] GameObject drinksMountoniousDewButton;
    [SerializeField] GameObject drinksBepisButton;
    [SerializeField] GameObject drinksSmallButton;
    [SerializeField] GameObject drinksMediumButton;
    [SerializeField] GameObject drinksLargeButton;
    [SerializeField] GameObject drinksDietButton;

    GameObject[] burgerButtons;
    GameObject[] hotDogButtons;
    GameObject[] friesButtons;
    GameObject[] drinksButtons;


    void Start() {

        currentOrder = new List<List<string>>();

        burgerButtons = new GameObject[] {burgerBunsButton, burgerSinglePattyButton, burgerDoublePattyButton,
            burgerCheeseButton, burgerLettuceButton, burgerOnionsButton, burgerPicklesButton, burgerBaconButton, burgerKetchupButton,
            burgerMustardButton, burgerMayoButton};

        hotDogButtons = new GameObject[] {hotDogBunButton, hotDogWienerButton, hotDogLettuceButton, hotDogSauerKrautButton,
            hotDogChiliButton, hotDogRelishButton, hotDogKetchupButton, hotDogMustardButton, hotDogMayoButton};

        friesButtons = new GameObject[] {friesSaltButton, friesCheeseButton, friesSmallButton, friesMediumButton, friesLargeButton};

        drinksButtons = new GameObject[] {drinksWorterButton, drinksSprightButton, drinksKongKolaButton, drinksDrCardboardButton,
            drinksMountoniousDewButton, drinksBepisButton, drinksSmallButton, drinksMediumButton, drinksLargeButton, drinksDietButton};

        receiptText.SetText("");
    }

    void Update() {

    }

    public void ShowBurgerMenu() {

        //initialize buttons
        burgerBunsButton.GetComponent<Toggle>().isOn = true;
        burgerSinglePattyButton.GetComponent<Toggle>().isOn = true;
        burgerDoublePattyButton.GetComponent<Toggle>().isOn = false;
        burgerCheeseButton.GetComponent<Toggle>().isOn = true;
        burgerLettuceButton.GetComponent<Toggle>().isOn = true;
        burgerOnionsButton.GetComponent<Toggle>().isOn = false;
        burgerPicklesButton.GetComponent<Toggle>().isOn = false;
        burgerBaconButton.GetComponent<Toggle>().isOn = false;
        burgerKetchupButton.GetComponent<Toggle>().isOn = true;
        burgerMustardButton.GetComponent<Toggle>().isOn = false;
        burgerMayoButton.GetComponent<Toggle>().isOn = false;

        burgerMenu.SetActive(true);
        rootMenu.SetActive(false);
    }

    public void BackFromBurgerMenu() {
        rootMenu.SetActive(true);
        burgerMenu.SetActive(false);
    }

    public void AddFromBurgerMenu() {
        AddToOrder("Burger", burgerButtons);
        rootMenu.SetActive(true);
        burgerMenu.SetActive(false);
    }

    public void ShowHotDogMenu() {

        //initialize buttons
        hotDogBunButton.GetComponent<Toggle>().isOn = true;
        hotDogWienerButton.GetComponent<Toggle>().isOn = true;
        hotDogLettuceButton.GetComponent<Toggle>().isOn = false;
        hotDogSauerKrautButton.GetComponent<Toggle>().isOn = false;
        hotDogChiliButton.GetComponent<Toggle>().isOn = false;
        hotDogRelishButton.GetComponent<Toggle>().isOn = false;
        hotDogKetchupButton.GetComponent<Toggle>().isOn = true;
        hotDogMustardButton.GetComponent<Toggle>().isOn = false;
        hotDogMayoButton.GetComponent<Toggle>().isOn = false;

        hotDogMenu.SetActive(true);
        rootMenu.SetActive(false);
    }

    public void BackFromHotDogMenu() {
        rootMenu.SetActive(true);
        hotDogMenu.SetActive(false);
    }

    public void AddFromHotDogMenu() {
        AddToOrder("Hot Dog", hotDogButtons);
        rootMenu.SetActive(true);
        hotDogMenu.SetActive(false);
    }

    public void ShowFriesMenu() {

        //initialize buttons
        friesSaltButton.GetComponent<Toggle>().isOn = true;
        friesCheeseButton.GetComponent<Toggle>().isOn = false;
        friesSmallButton.GetComponent<Toggle>().isOn = false;
        friesMediumButton.GetComponent<Toggle>().isOn = true;
        friesLargeButton.GetComponent<Toggle>().isOn = false;

        friesMenu.SetActive(true);
        rootMenu.SetActive(false);
    }
    public void BackFromFriesMenu() {
        rootMenu.SetActive(true);
        friesMenu.SetActive(false);
    }

    public void AddFromFriesMenu() {
        AddToOrder("Fries", friesButtons);
        rootMenu.SetActive(true);
        friesMenu.SetActive(false);
    }

    public void ShowDrinksMenu() {

        //initialize buttons
        drinksWorterButton.GetComponent<Toggle>().isOn = true;
        drinksSprightButton.GetComponent<Toggle>().isOn = false;
        drinksKongKolaButton.GetComponent<Toggle>().isOn = false;
        drinksDrCardboardButton.GetComponent<Toggle>().isOn = false;
        drinksMountoniousDewButton.GetComponent<Toggle>().isOn = false;
        drinksBepisButton.GetComponent<Toggle>().isOn = false;
        drinksSmallButton.GetComponent<Toggle>().isOn = false;
        drinksMediumButton.GetComponent<Toggle>().isOn = true;
        drinksLargeButton.GetComponent<Toggle>().isOn = false;
        drinksDietButton.GetComponent<Toggle>().isOn = false;

        drinksMenu.SetActive(true);
        rootMenu.SetActive(false);
    }
    public void BackFromDrinksMenu() {
        rootMenu.SetActive(true);
        drinksMenu.SetActive(false);
    }

    public void AddFromDrinksMenu() {
        AddToOrder("Drink", drinksButtons);
        rootMenu.SetActive(true);
        drinksMenu.SetActive(false);
    }

    public void AddToOrder(string itemType, GameObject[] buttonsType) {

        List<string> currentItem = new List<string>();

        currentOrder.Add(currentItem);
        currentOrder[currentOrder.Count - 1].Add(itemType);

        foreach (GameObject thing in buttonsType) {
            if (thing.GetComponent<Toggle>().isOn == true) {
                currentOrder[currentOrder.Count - 1].Add(thing.name);
            }
        }

        UpdateReceiptScreen();
    }

    public void UpdateReceiptScreen() {

        string formattedReceipt = "";

        if (currentOrder.Count <= 0) { //this is for clearing
            receiptText.SetText(formattedReceipt);
            return;
        }

        for (int x = 0; x < currentOrder.Count; x++) {
            formattedReceipt = formattedReceipt + currentOrder[x][0] + "\n";

            for (int i = 1; i < currentOrder[x].Count; i++) {
                formattedReceipt = formattedReceipt + "    " + currentOrder[x][i] + "\n";
            }

            formattedReceipt = formattedReceipt + "\n";
        }
        
        receiptText.SetText(formattedReceipt);

    }

    public void CheckOrder() {
        /* the idea here is that this will check to see if the current order has all of the contents of the correct order
         * the ordering should not matter
         * 
         * look at the first element in the correct order list
         * see if currentOrder contains that element
         * if not, fail and do the failure methods, then return
         * if so, remove that element from the currentOrder list
         * 
         * repeat until the final item is checked from the correct order
         * then check if currentOrder.Count == 0
         * if it does, pass
         * if not, fail
         */
    }

}
