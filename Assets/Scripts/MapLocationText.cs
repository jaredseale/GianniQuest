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
                locationName.text = "Home House";
                break;

            case "C":
                locationName.text = "Ethereal Ascent";
                break;

            case "J":
                locationName.text = "Pizza Hell";
                break;

            case "N":
                locationName.text = "The Sewers";
                break;

            case "S":
                locationName.text = "Rancid Rick's";
                break;

            case "T":
                locationName.text = "Le Cul Puant";
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
