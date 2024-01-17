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

    public ShopItem BuyItem(ShopItem item)
    {
        ShopItem selectedItem = FindItemByID(item.id);
        return selectedItem;
    }

    private ShopItem FindItemByID(int itemID)
    {
        foreach (var item in shopItems)
        {
            if (item.id == itemID)
            {
                return item;
            }
        }
        return null;
    }
}


[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}