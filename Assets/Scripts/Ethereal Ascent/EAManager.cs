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
    [SerializeField] GameObject cloudSpawner;
    [SerializeField] GameObject cloud;
     
    void Start() {
        if (PlayerPrefs.GetString("SantaDialogueState") == "Init2" || PlayerPrefs.GetString("SantaDialogueState") == "PreDollar") {
            itemCounter.GetComponent<Animator>().SetTrigger("itemCounterSlide");
        }

        itemCount = 0;

        for (int i = 0; i < 200; i += 1) {
            Instantiate(cloudSpawner, new Vector3(-60f, (float) i, -5f), Quaternion.identity, gameObject.transform);
        }

        InitialCloudPlacement();

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

    private void InitialCloudPlacement() {
        for (int i = 0; i < 150; i++) {
            float xVal = Random.Range(-60f, 65f);
            float yVal = Random.Range(-5f, 100f);
            Instantiate(cloud, new Vector3(xVal, yVal, 0f), Quaternion.identity, gameObject.transform);
        }
    }
}
