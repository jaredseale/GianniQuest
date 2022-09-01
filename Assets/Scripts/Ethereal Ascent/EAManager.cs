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
    [SerializeField] GameObject[] stars;
    [SerializeField] GameObject itemGroup;

    void Start() {
        if (PlayerPrefs.GetString("SantaDialogueState") == "Init2" || PlayerPrefs.GetString("SantaDialogueState") == "PreDollar") {
            itemCounter.GetComponent<Animator>().SetTrigger("itemCounterSlide");
        }

        if (PlayerPrefs.GetString("SantaDialogueState") == "PreDollar" || PlayerPrefs.GetString("SantaDialogueState") == "PostDollar") {
            DespawnItems();
        }

        itemCount = 0;

        for (int i = 0; i < 140; i += 1) {
            Instantiate(cloudSpawner, new Vector3(-70f, (float) i, -5f), Quaternion.identity, gameObject.transform);
        }

        InitialCloudPlacement();
        InitialStarPlacement();

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

        for (int i = 0; i < 25; i++) {
            float xVal = Random.Range(-60f, 65f);
            float yVal = Random.Range(100f, 140f); //come back and adjust this once i have final max y value
            Instantiate(cloud, new Vector3(xVal, yVal, 0f), Quaternion.identity, gameObject.transform);
        }
    }
    private void InitialStarPlacement() {
        for (int i = 0; i < 400; i++) {
            float xVal = Random.Range(-60f, 70f);
            float yVal = Random.Range(130f, 200f); //adjust these once you have every in its final place
            Instantiate(stars[Random.Range(0, 3)], new Vector3(xVal, yVal, 0f), Quaternion.identity, gameObject.transform);
        }
    }

    private void DespawnItems() {
        itemGroup.SetActive(false);
    }
}
