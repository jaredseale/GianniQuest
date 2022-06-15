using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDictionary : MonoBehaviour
{

    public List<List<string>> OrderDetails(int orderNumber) {

        List<List<string>> order = new List<List<string>>();

        switch (orderNumber) {

            case 1:
                order = new List<List<string>> { 
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"}, 
                };
                break;

            case 2:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Ketchup"},
                    new List<string> {"Drink", "Worter", "Medium"}
                };
                break;

            default:
                Debug.Log("order number does not exist");
                break;
        }

        return order;
    }

}
