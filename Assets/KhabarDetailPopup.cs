using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KhabarDetailPopup : MonoBehaviour
{
    private int id, price;
    private bool isBought;
    private Action<int, bool> buyItemCallback;

    public void ShowDetails(
        ShopManager.ShopItem shopItem,
        Action<int, bool> callback
        )
    {
        id = shopItem.id;
        price = shopItem.Price;
        isBought = shopItem.IsPurchased;
        buyItemCallback = callback;
        Image targetImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();

        targetImage.sprite = shopItem.Image;
        //targetImage.SetNativeSize();

        transform.GetChild(1).GetComponent<TMP_Text>().text = shopItem.descriptionText;

        
        UpdateBuyButton();
        

        gameObject.SetActive(true);
    }

    public void BuyItem()
    {
        int currentCoins = CoinManager.GetCoins();

        if (currentCoins > price && currentCoins > 0)
        {
            CoinManager.RemoveCoins(price);
            isBought = true;
            buyItemCallback(id, isBought);

            UpdateBuyButton();
        }

        // else not enough coins 
    }

    private void UpdateBuyButton()
    {
        Button buyButton = transform.GetChild(2).GetChild(0).GetComponent<Button>();
        
        if (!isBought) {
            buyButton.interactable = true;
            buyButton.GetComponentInChildren<TMP_Text>().text = "ПРИДБАТИ";
            
        } else
        {
            buyButton.interactable = false;
            buyButton.GetComponentInChildren<TMP_Text>().text = "ВЖЕ ПРИДБАНО";
        }
        
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

}
