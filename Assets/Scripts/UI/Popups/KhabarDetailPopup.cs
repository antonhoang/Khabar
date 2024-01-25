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

    public GameObject supportUs;
    public GameObject supportUA;

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

    public void SupportUs()
    {
        supportUs.SetActive(true);
    }

    public void SupportUkraine()
    {
        supportUA.SetActive(true);
    }

    public void SupportUsAction()
    {
        Application.OpenURL("https://www.buymeacoffee.com/5xcnzc9ln0/");
    }

    public void SupportUsBankAction()
    {
        Application.OpenURL("https://send.monobank.ua/jar/3eGqY9Zxn5");
    }

    public void SupportUkraineAction()
    {
        Application.OpenURL("https://u24.gov.ua/uk");
    }

    public void CloseSupportUs()
    {
        supportUs.SetActive(false);
    }

    public void CloseSupportUA()
    {
        supportUA.SetActive(false);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

}
