using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapLocationText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI locationName;
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

                if (PlayerPrefs.GetString("DateProgress") == "End") {
                    locationName.text = "GBGL-XX1";
                }
                break;

            case "C1":
                locationName.text = "Pizza Hell";
                break;

            case "J":
                locationName.text = "Rancid Rick's";
                break;

            case "N4":
                locationName.text = "Ethereal Ascent";
                break;

            case "S":
                locationName.text = "Home House";
                break;

            case "T":
                locationName.text = "The Sewers";
                break;

            case "V":
                locationName.text = "High School High";
                break;

            case "W":
                locationName.text = "SNICO";
                break;
        }
    }
}
