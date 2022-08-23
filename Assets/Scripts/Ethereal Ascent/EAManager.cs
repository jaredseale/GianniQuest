using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EAManager : MonoBehaviour
{

    [SerializeField] GameObject itemCounter;
    [SerializeField] TextMeshProUGUI itemCountText;
    public int itemCount;
    public SpriteRenderer itemCollectionIcon;
    public TextMeshProUGUI itemCollectionText;
    [SerializeField] Animator itemCollectionBox;


    void Start() {
        if (PlayerPrefs.GetString("SantaDialogueState") == "Init2" || PlayerPrefs.GetString("SantaDialogueState") == "PreDollar") {
            itemCounter.GetComponent<Animator>().SetTrigger("itemCounterSlide");
        }

        itemCount = 0;
        
    }

    void Update() {
        itemCountText.text = itemCount.ToString() + "/20";
        
    }

    public void ItemCounterSlideIn() {
        itemCounter.GetComponent<Animator>().SetTrigger("itemCounterSlide");
    }

    public void ItemCollectionSlideIn() {
        itemCollectionBox.SetTrigger("giftFound");
    }
}
