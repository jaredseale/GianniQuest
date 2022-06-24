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

            case 3:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Ketchup"},
                    new List<string> {"Fries", "Salt", "Medium"}
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

            case 6:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Hot Dog", "Wiener", "Ketchup"},
                    new List<string> {"Fries", "Salt", "Cheese", "Large"}
                };
                break;

            case 7:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce"},
                    new List<string> {"Drink", "Worter", "Medium"},
                    new List<string> {"Drink", "Worter", "Medium"}
                };
                break;

            case 8:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup"}
                };
                break;

            case 9:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Ketchup", "Mayo"},
                    new List<string> {"Fries", "Salt", "Small"},
                    new List<string> {"Drink", "Bepis", "Medium", "Diet"}
                };
                break;

            case 10:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Double Patty", "Cheese", "Lettuce", "Onions", "Pickles", "Bacon", "Ketchup", "Mayo"},
                    new List<string> {"Drink", "Dr. Cardboard", "Small"}
                };
                break;

            case 11:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Chili", "Ketchup"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Lettuce", "Ketchup"},
                    new List<string> {"Fries", "Salt", "Large"},
                    new List<string> {"Drink", "Mountonious Dew", "Large"}
                };
                break;

            case 12:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Ketchup", "Mustard", "Mayo"},
                    new List<string> {"Fries", "Salt", "Small"},
                    new List<string> {"Drink", "Kong Kola", "Small", "Diet"},
                };
                break;

            case 13:
                order = new List<List<string>> {
                    new List<string> {"Drink", "Bepis", "Medium"},
                    new List<string> {"Drink", "Bepis", "Medium"},
                    new List<string> {"Drink", "Bepis", "Medium"},
                    new List<string> {"Drink", "Dr. Cardboard", "Large"},
                    new List<string> {"Drink", "Worter", "Small"}
                };
                break;

            case 14:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Lettuce", "Sauer Kraut", "Chili", "Relish", "Ketchup", "Mustard", "Mayo"},
                    new List<string> {"Fries", "Salt", "Cheese", "Large"},
                    new List<string> {"Drink", "Worter", "Medium", "Diet"}
                };
                break;

            case 15:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Wiener"}
                };
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

            case 19:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Relish", "Mayo"},
                    new List<string> {"Fries", "Large"},
                    new List<string> {"Drink", "Kong Kola", "Medium", "Diet"}
                };
                break;

            case 20:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Onions", "Pickles", "Bacon", "Ketchup", "Mayo"},
                    new List<string> {"Fries", "Salt", "Small"},
                    new List<string> {"Fries", "Salt", "Small"},
                    new List<string> {"Drink", "Kong Kola", "Large"}
                };
                break;

            case 21:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Lettuce", "Ketchup", "Mustard", "Mayo"},
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Ketchup", "Mustard", "Mayo"},
                    new List<string> {"Drink", "Worter", "Small"}
                };
                break;

            case 22:
                order = new List<List<string>> {
                    new List<string> {"Drink", "Spright", "Small"},
                    new List<string> {"Drink", "Spright", "Medium"},
                    new List<string> {"Drink", "Spright", "Large"},
                    new List<string> {"Fries", "Salt", "Large"},
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Ketchup"},
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Sauer Kraut", "Ketchup"},
                };
                break;

            case 23:
                order = new List<List<string>> {
                    new List<string> {"Drink", "Kong Kola", "Small"},
                    new List<string> {"Drink", "Mountonious Dew", "Large", "Diet"},
                    new List<string> {"Drink", "Spright", "Medium", "Diet"},
                    new List<string> {"Drink", "Worter", "Medium"},
                    new List<string> {"Drink", "Bepis", "Large"},
                    new List<string> {"Drink", "Dr. Cardboard", "Small"}
                };
                break;

            case 24:
                order = new List<List<string>> {
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Lettuce", "Chili", "Ketchup", "Mustard"},
                    new List<string> {"Burger", "Buns", "Double Patty", "Cheese", "Lettuce", "Onions", "Bacon", "Ketchup", "Mayo"},
                };
                break;

            case 25:
                order = new List<List<string>> {
                    new List<string> {"Burger", "Buns", "Single Patty", "Cheese", "Pickles", "Mustard", "Mayo"},
                    new List<string> {"Burger", "Buns", "Single Patty", "Lettuce", "Ketchup"},
                    new List<string> {"Hot Dog", "Bun", "Wiener", "Sauer Kraut"},
                    new List<string> {"Fries", "Medium"},
                    new List<string> {"Fries", "Salt", "Cheese", "Small"},
                    new List<string> {"Drink", "Dr. Cardboard", "Small", "Diet"},
                    new List<string> {"Drink", "Bepis", "Medium"}
                };
                break;

            default:
                Debug.Log("order number does not exist");
                break;
        }

        return order;
    }

}
