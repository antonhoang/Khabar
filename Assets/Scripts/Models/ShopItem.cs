using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "ScriptableObjects/ShopItemSO", order = 1)]
public class ShopItem : ScriptableObject
{
    public int id;
    public Sprite Image;
    public int price;
    public string title;
    public bool isPurchased;
    public string descriptionText;
}
