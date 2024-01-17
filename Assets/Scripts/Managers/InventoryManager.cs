using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventoryManager
{
    public List<ShopItem> items;

    public InventoryManager()
    {
        items = new List<ShopItem>();
    }

    public bool AddItem(ShopItem item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveItem(ShopItem item)
    {
        return items.Remove(item);
    }

    public bool ContainsItem(ShopItem item)
    {
        return items.Contains(item);
    }
}