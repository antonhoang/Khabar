using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public List<ShopItem> shopItems;
    public InventoryManager playerInventory;

    // Other shop-related functionality can be added here

    private void Start()
    {
        // Example: Load items from Scriptable Objects
        LoadShopItems();
        playerInventory = new InventoryManager();
    }

    void LoadShopItems()
    {
        // Clear the existing list
        shopItems.Clear();

        // Load Scriptable Objects from the Resources folder
        ShopItem[] items = Resources.LoadAll<ShopItem>("ShopItems");

        // Add loaded items to the list
        shopItems.AddRange(items);
    }

    public bool BuyItem(ShopItem item)
    {
        int currentCoins = CoinManager.GetCoins();
        // Check if the item is not already in the inventory and player has enough coins
        if (!playerInventory.ContainsItem(item) && currentCoins >= item.itemPrice)
        {
            // Deduct the item price from player coins
            currentCoins -= item.itemPrice;

            // Add the item to the player's inventory
            playerInventory.AddItem(item);

            Debug.Log("Item bought: " + item.itemName);
            return true; // Item bought successfully
        }
        else
        {
            Debug.Log("Unable to buy the item: " + item.itemName);
            return false; // Unable to buy the item
        }
    }
}


[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}