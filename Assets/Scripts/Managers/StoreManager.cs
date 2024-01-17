using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour
{

    ShopManager shop;
    InventoryManager inventory;
    // Use this for initialization

    void Start()
    {
        shop = new ShopManager();
        inventory = new InventoryManager();
        InitializeInventory();
    }

    // Update is called once per frame
    void Update()
    { 

    }

    public bool BuyItem(ShopItem item)
    {
        // Check if the item is not already in the inventory and player has enough coins
        if (!inventory.ContainsItem(item) && CoinManager.GetCoins() >= item.itemPrice)
        {
            CoinManager.RemoveCoins(item.itemPrice);
            shop.BuyItem(item);
            inventory.AddItem(item);

            Debug.Log("Item bought: " + item.itemName);
            return true; // Item bought successfully
        }
        else
        {
            Debug.Log("Unable to buy the item: " + item.itemName);
            return false; // Unable to buy the item
        }
    }

    private void InitializeInventory()
    {
        // Add previously bought items to the inventory
        foreach (ShopItem item in inventory.items)
        {
            Debug.Log("Initializing inventory with item: " + item.itemName);
        }
    }
}
