using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "ShopItemList", menuName = "ScriptableObjects/ShopItemList", order = 1)]
public class ShopItemList : ScriptableObject
{
    public List<ShopItem> items;
}