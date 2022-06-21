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

            case 4:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Onions", "Pickles", "Ketchup"},
                    new List<string> {"Drink", "Kong Kola", "Medium"}
                };
                break;

            case 5:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Relish", "Ketchup"},
                    new List<string> {"Fries", "Medium"},
                    new List<string> {"Drink", "Spright", "Medium"}
                };
                break;

            case 15:
                order = new List<List<string>> {};
                break;

            case 16:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Pickles"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Pickles", "Ketchup"},
                    new List<string> {"Fries", "Small"},
                    new List<string> {"Drink", "Dr. Cardboard", "Small", "Diet"}
                };
                break;

            case 17:
                order = new List<List<string>> {
                    new List<string> {"Drink", "Mountonious Dew", "Medium"},
                    new List<string> {"Burger", "Buns", "Double Patty", "Cheese", "Lettuce", "Onions", "Bacon", "Ketchup"},
                    new List<string> {"Fries", "Salt", "Medium"}
                };
                break;

            default:
                Debug.Log("order number does not exist");
                break;
        }

        return order;
    }

}
