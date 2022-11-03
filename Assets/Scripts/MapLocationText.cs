using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapLocationText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI locationName;
    [SerializeField] SpriteRenderer locationColor;
    [SerializeField] OverworldPlayer player;
    [SerializeField] GameObject locationDisplay;

    void Update() {
        DisplayLocation();
    }

    private void DisplayLocation() {
        if (!player.playerInTransit && player.currentWaypoint.GetComponent<OverworldWaypoints>().isLocation) {
            locationDisplay.SetActive(true);
        } else {
            locationDisplay.SetActive(false);
        }

        string playerLocation = player.currentWaypoint.gameObject.name;

        switch (playerLocation) {

            case "A":
                locationName.text = "Le Cul Puant";
                locationColor.color = new Color(200/255f, 106/255f, 0/255f);

                if (PlayerPrefs.GetString("DateProgress") == "End") {
                    locationName.text = "GBGL-XX1";
                }
                break;

            case "C1":
                locationName.text = "Pizza Hell";
                locationColor.color = new Color(0f, 0f, 0f);
                break;

            case "J":
                locationName.text = "Rancid Rick's";
                locationColor.color = new Color(0f, 200/255f, 33/255f);
                break;

            case "N4":
                locationName.text = "Ethereal Ascent";
                locationColor.color = new Color(0f, 148/255f, 200/255f);
                break;

            case "S":
                locationName.text = "Home House";
                locationColor.color = new Color(0f, 0f, 0f);
                break;

            case "T":
                locationName.text = "The Sewers";
                locationColor.color = new Color(55/255f, 0f, 206/255f);
                break;

            case "V":
                locationName.text = "High School High";
                locationColor.color = new Color(200/255f, 212/255f, 0f);
                break;

            case "W":
                locationName.text = "SNICO";
                locationColor.color = new Color(200/255f, 0f, 0f);
                break;
        }
    }
}
