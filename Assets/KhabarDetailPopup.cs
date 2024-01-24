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
        Sprite image,
        int id,
        int price,
        string descriptionText,
        bool isBought,
        Action<int, bool> callback
        )
    {        
        this.id = id;
        this.price = price;
        this.isBought = isBought;
        buyItemCallback = callback;
        Image targetImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        
        targetImage.sprite = image;
        //targetImage.SetNativeSize();

        transform.GetChild(1).GetComponent<TMP_Text>().text = descriptionText;
        gameObject.SetActive(true);
    }

    

    public void BuyItem()
    {
        int currentCoins = CoinManager.GetCoins();

        if (isBought)
        {
            Button buyButton = transform.GetChild(2).GetChild(0).GetComponent<Button>();
            buyButton.interactable = !isBought;
            buyButton.GetComponentInChildren<TMP_Text>().text = "ВЖЕ ПРИДБАНО";
            //transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "ВЖЕ ПРИДБАНО";
        }

        if (currentCoins > price && currentCoins > 0)
        {
            CoinManager.RemoveCoins(price);
            isBought = true;
            buyItemCallback(id, isBought);
        }

        // else not enough coins 
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

}
