using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class OrderManager : MonoBehaviour
{
    public List<List<string>> currentOrder;

    [SerializeField] OrderState currentOrderState;
    [SerializeField] OrderState[] orderStateArray;
    int orderStateArrayIndex;

    [Space(30)]

    public int health;
    [SerializeField] Image healthBar;
    [SerializeField] Animator healthBarAnimator;
    [SerializeField] Animator carAnimator;
    [SerializeField] SpriteRenderer carSprite;
    [SerializeField] AudioSource carAudio;
    [SerializeField] AudioClip slideWhistleDown;
    [SerializeField] AudioClip slideWhistleUp;
    [SerializeField] TextMeshProUGUI receiptText;
    [SerializeField] Scrollbar receiptScroll;
    [SerializeField] GameObject loseScreen;
    [SerializeField] AudioClip loseSFX;
    [SerializeField] GameObject winScreen;
    [SerializeField] AudioClip winSFX;

    [Space(30)]

    [SerializeField] Button infoButton;
    [SerializeField] Button nextCustomerButton;
    [SerializeField] Button repeatOrderButton;
    [SerializeField] Button clearOrderButton;
    [SerializeField] Button completeOrderButton;
    [SerializeField] GameObject rootMenu;
    [SerializeField] GameObject burgerMenu;
    [SerializeField] GameObject hotDogMenu;
    [SerializeField] GameObject friesMenu;
    [SerializeField] GameObject drinksMenu;

    [Space(30)]

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

    [Space(30)]

    [SerializeField] GameObject hotDogBunButton;
    [SerializeField] GameObject hotDogWienerButton;
    [SerializeField] GameObject hotDogLettuceButton;
    [SerializeField] GameObject hotDogSauerKrautButton;
    [SerializeField] GameObject hotDogChiliButton;
    [SerializeField] GameObject hotDogRelishButton;
    [SerializeField] GameObject hotDogKetchupButton;
    [SerializeField] GameObject hotDogMustardButton;
    [SerializeField] GameObject hotDogMayoButton;

    [Space(30)]

    [SerializeField] GameObject friesSaltButton;
    [SerializeField] GameObject friesCheeseButton;
    [SerializeField] GameObject friesSmallButton;
    [SerializeField] GameObject friesMediumButton;
    [SerializeField] GameObject friesLargeButton;

    [Space(30)]

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

    [Space(30)]

    GameObject[] burgerButtons;
    GameObject[] hotDogButtons;
    GameObject[] friesButtons;
    GameObject[] drinksButtons;

    OrderDictionary orderDictionary;
    bool inProgressOrder;
    AudioSource voiceAudio;

    void Start() {

        orderDictionary = GetComponent<OrderDictionary>();

        if (PlayerPrefs.GetString("SNICOProgress") == "FailedAfterCheckpoint") {
            orderStateArrayIndex = 10;
        } else {
            orderStateArrayIndex = 0;
        }

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
        healthBar.fillAmount = (health / 100f);

        voiceAudio = GetComponent<AudioSource>();

        repeatOrderButton.interactable = false;
        completeOrderButton.interactable = false;

    }

    void Update() {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / 100f), Time.deltaTime);

        if (currentOrder.Count > 0) {
            clearOrderButton.interactable = true;
        } else {
            clearOrderButton.interactable = false;
        }
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
        StartCoroutine("ScrollToBottom");

    }

    IEnumerator ScrollToBottom() {
        yield return new WaitForSeconds(.05f); //this lets it realize its new value before it starts scrolling

        float totalTime = 0.5f;
        float currentTime = 0f;
        float currentValue = receiptScroll.value;

        while (receiptScroll.value > 0f) {
            
            currentTime += Time.deltaTime;
            receiptScroll.value = Mathf.Lerp(currentValue, 0f, currentTime / totalTime);
            yield return null;
        }

        receiptScroll.value = 0f;
        
    }

    public void BeginOrder() { //this is the Next Customer button
        if (!inProgressOrder) {
            currentOrderState = orderStateArray[orderStateArrayIndex];
            inProgressOrder = true;
            ClearOrder();

            nextCustomerButton.interactable = false;

            carSprite.sprite = currentOrderState.vehicleSprite;

            if (orderStateArrayIndex == 20) { //toes 2nd order
                carAnimator.SetTrigger("balloonEnter");
                carAudio.PlayOneShot(slideWhistleDown);
            } else if (orderStateArrayIndex == 24) { //sheriff's 2nd order
                carAnimator.SetTrigger("finalOrderEnter");
                StartCoroutine("FinalOrderCarAudio");
            } else {
                carAnimator.SetTrigger("carEnter");
                carAudio.pitch = Random.Range(0.7f, 1.3f); //give the sounds some variety
                carAudio.Play();
            }

            carAudio.panStereo = 1f;
            StartCoroutine("CarAudioPanStart");
            StartCoroutine("DelayedOrderAudio");
        }
    }

    IEnumerator DelayedOrderAudio() {
        voiceAudio.Stop();
        if (orderStateArrayIndex == 24) { //if it's the final order
            yield return new WaitForSeconds(7f);
        } else {
            yield return new WaitForSeconds(3f);
        }
        voiceAudio.PlayOneShot(currentOrderState.orderAudio);
        repeatOrderButton.interactable = true;
        completeOrderButton.interactable = true;
    }

    IEnumerator FinalOrderCarAudio() {
        carAudio.pitch = Random.Range(0.7f, 1.3f);
        carAudio.Play();
        yield return new WaitForSeconds(4f);
        carAudio.pitch = Random.Range(0.7f, 1.3f);
        carAudio.Play();
    }

    public void RepeatOrder() {
        if (inProgressOrder) {
            health -= 3;
            if (health < 0) {
                health = 0;
            }
            voiceAudio.Stop();
            voiceAudio.PlayOneShot(currentOrderState.orderAudio);
        }
    }

    public void ClearOrder() {
        currentOrder.Clear();
        UpdateReceiptScreen();
    }

    public void CompleteOrder() {
        if (inProgressOrder) {
            voiceAudio.Stop();
            if (CheckOrder() == true) {
                Debug.Log("Order was correct!");
                healthBarAnimator.SetTrigger("healthUp");
                health += 5;
                if (health > 100) {
                    health = 100;
                }
            } else {
                Debug.Log("Order was NOT correct!");
                healthBarAnimator.SetTrigger("healthDown");
                health -= 20;
                if (health < 0) {
                    health = 0;
                }

            }

            StopCoroutine("ScrollToBottom");
            StopCoroutine("DelayedOrderAudio");
            StopCoroutine("FinalOrderCarAudio");

            ClearOrder();

            repeatOrderButton.interactable = false;
            completeOrderButton.interactable = false;

            StartCoroutine("CarAudioPanEnd");
            inProgressOrder = false;

            if (orderStateArrayIndex == 20) { //toes 2nd order
                carAnimator.SetTrigger("balloonExit");
                carAudio.PlayOneShot(slideWhistleUp);
            } else if (orderStateArrayIndex == 24 && health > 0) { //win sequence!!!
                infoButton.interactable = false;
                carAnimator.SetTrigger("finalOrderExit");
                carAudio.Play();
                PlayerPrefs.SetString("SNICOProgress", "Complete");
                PlayerPrefs.SetString("SNICOEntry", "Done");
                PlayerPrefs.SetString("DadDialogueState", "PreDollar");
                winScreen.SetActive(true);
                voiceAudio.PlayOneShot(winSFX);
            } else {
                carAnimator.SetTrigger("carExit");
                carAudio.Play();
            }

            if (health <= 0) { //lose sequence
                infoButton.interactable = false;
                voiceAudio.PlayOneShot(loseSFX);
                loseScreen.SetActive(true);
                if (orderStateArrayIndex < 10) {
                    PlayerPrefs.SetString("SNICOProgress", "FailedBeforeCheckpoint");
                } else {
                    PlayerPrefs.SetString("SNICOProgress", "FailedAfterCheckpoint");
                }

                PlayerPrefs.SetString("DadDialogueState", "FailedSNICO");

                return;
            }

            orderStateArrayIndex++;

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

    IEnumerator CarAudioPanStart() {
        float totalTime = 3;
        float currentTime = 0;

        while (carAudio.panStereo > -0.15f) {
            currentTime += Time.deltaTime;
            carAudio.panStereo = Mathf.Lerp(1f, -0.2f, currentTime / totalTime);
            yield return null;
        }
    }

    IEnumerator CarAudioPanEnd() {
        float totalTime = 3;
        float currentTime = 0;

        while (carAudio.panStereo > -0.95f) {
            currentTime += Time.deltaTime;
            carAudio.panStereo = Mathf.Lerp(-0.2f, -1f, currentTime / totalTime);
            yield return null;
        }
        if (orderStateArrayIndex <= orderStateArray.Length - 1) {
            if (health > 0) {
                nextCustomerButton.interactable = true;
            }
        }
    }

}
