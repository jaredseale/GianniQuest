using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{

    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject dataMenu;


    public void BackToMainButtons() {
        mainButtons.SetActive(true);
        dataMenu.SetActive(false);
    }

}
