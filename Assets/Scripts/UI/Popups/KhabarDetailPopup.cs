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
    private Action<bool> backButtonCallback;

    public GameObject supportUs;
    public GameObject supportUA;

    public void ShowDetails(
        ShopItem shopItem,
        Action<int, bool> callback,
        Action<bool> backButtonCallback
        )
    {
        id = shopItem.id;
        price = shopItem.price;
        isBought = shopItem.isPurchased;
        buyItemCallback = callback;
        this.backButtonCallback = backButtonCallback;

        SetImageTarget(shopItem);
        SetTextDescription(shopItem);
        SetTextPrice(shopItem);
        UpdateBuyButton();
        

        gameObject.SetActive(true);
    }

    void SetImageTarget(ShopItem shopItem)
    {
        Image targetImage = transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        targetImage.sprite = shopItem.Image;
    } 

    void SetTextDescription(ShopItem shopItem)
    {
        TMP_Text targetText = transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        targetText.text = shopItem.descriptionText;
    }

    void SetTextPrice(ShopItem shopItem)
    {
        
        TMP_Text targetText = transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>();

        PriceFormatter priceFormatter = new PriceFormatter();
        targetText.text = priceFormatter.FormatPrice(shopItem.price); 
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
            buyButton.GetComponentInChildren<TMP_Text>().text = "Придбати";
            
        } else
        {
            buyButton.interactable = false;
            buyButton.GetComponentInChildren<TMP_Text>().text = "Вже придбано";
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
        Application.OpenURL("https://send.monobank.ua/jar/8ZJHnKVu58");
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
        backButtonCallback(true);
        gameObject.SetActive(false);
    }

}
