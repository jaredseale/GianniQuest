using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class OrderManager : MonoBehaviour
{
    public List<List<string>> currentOrder;

    [SerializeField] OrderState[] orderStateArray;
    int orderStateArrayIndex;
    [SerializeField] OrderState currentOrderState;

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

    OrderDictionary orderDictionary;
    bool inProgressOrder;
    public int health;

    void Start() {

        orderDictionary = GetComponent<OrderDictionary>();

        orderStateArrayIndex = 0; //come back here later and initialize this based on checkpoints

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
        inProgressOrder = false;
        health = 80;

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
            formattedReceipt = formattedReceipt + (x + 1) + ". " + currentOrder[x][0] + "\n";

            for (int i = 1; i < currentOrder[x].Count; i++) {
                formattedReceipt = formattedReceipt + "    " + currentOrder[x][i] + "\n";
            }

            formattedReceipt = formattedReceipt + "\n";
        }
        
        receiptText.SetText(formattedReceipt);

    }

    public void BeginOrder() {
        Debug.Log("Beginning order " + (orderStateArrayIndex + 1));
        currentOrderState = orderStateArray[orderStateArrayIndex];
        inProgressOrder = true;
        ClearOrder();
    }

    public void ClearOrder() {
        currentOrder.Clear();
        UpdateReceiptScreen();
    }

    public void CompleteOrder() {
        if (inProgressOrder) {
            if (CheckOrder() == true) {
                Debug.Log("Order was correct!");
                health += 5;
                if (health > 100) {
                    health = 100;
                }
            } else {
                Debug.Log("Order was NOT correct!");
                health -= 20;
                if (health < 0) {
                    health = 0;
                }
            }

            Debug.Log("Health is now " + health);
            ClearOrder();
            orderStateArrayIndex++;
            inProgressOrder = false;
        }
    }

    public bool CheckOrder() {
        // build the comparer for comparing the inner lists
        var innerComparer = new ListComparer<string>();

        // ensure the lists have the same number of items
        if (currentOrder.Count != orderDictionary.OrderDetails(currentOrderState.orderNumber).Count) {
            return false;
        }

        foreach (var order in orderDictionary.OrderDetails(currentOrderState.orderNumber)) {
            // if an inner list doesn't also exist in testOrder2
            // then return false
            if (!currentOrder.Contains(order, innerComparer)) {
                return false;
            }
        }
        // all lists were found, so return true
        return true;
    }

    public class ListComparer<TItem> : IEqualityComparer<IList<TItem>>
    {
        private readonly IEqualityComparer<TItem> _itemComparer;

        public ListComparer()
            : this(EqualityComparer<TItem>.Default) {

        }

        public ListComparer(IEqualityComparer<TItem> itemComparer) {
            _itemComparer = itemComparer;
        }

        // Determine if the two lists have the same content
        public bool Equals(IList<TItem> x, IList<TItem> y) {
            if (Object.ReferenceEquals(x, y)) {
                return true;
            } else if (x is null != y is null) {
                return false;
            } else if (x is null && y is null) {
                return true;
            }

            return x.SequenceEqual(y, _itemComparer);
        }

        // Generate a hashcode from the individual items of the list
        // this is dependent on the order of the items
        // ({A,B} will produce a different value to {B,A})
        public int GetHashCode(IList<TItem> obj) {
            unchecked {
                int hashCode = 63949;
                foreach (var item in obj) {
                    hashCode += _itemComparer.GetHashCode(item);
                    hashCode *= 13;
                }
                return hashCode;
            }
        }
    }

}
