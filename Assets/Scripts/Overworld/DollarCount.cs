using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DollarCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dollarCount;

    void Start() {
        if (PlayerPrefs.GetInt("Dollars") == 0) {
            this.gameObject.SetActive(false);
        }

        dollarCount.text = PlayerPrefs.GetInt("Dollars").ToString();

    }

}
