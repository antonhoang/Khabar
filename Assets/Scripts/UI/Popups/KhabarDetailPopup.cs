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

    public TMP_Text notEnoughMoneyLabel;

    public GameObject supportUs;
    public GameObject supportUA;

    public List<GameObject> coinLabels;

    private List<ShakeEffect> shakeEffects = new List<ShakeEffect>();

    private void Awake()
    {
        foreach (GameObject coinLabel in coinLabels)
        {
            ShakeEffect shakeEffect = coinLabel.GetComponent<ShakeEffect>();

            if (shakeEffect != null)
            {
                shakeEffects.Add(shakeEffect);
            }
            else
            {
                Debug.LogError($"ShakeEffect script not found on {coinLabel.name} GameObject.");
            }
        }
    }

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
        } else
        {
            //ShowNotEnoughMoneyLabel();
            
            StartCoroutine(ShowNotEnoughMoneyLabel());
            ShakeCoinLabels();
        }

        // else not enough coins 
    }

    private System.Collections.IEnumerator ShowNotEnoughMoneyLabel()
    {
        // Display the label indicating not enough money
        notEnoughMoneyLabel.gameObject.SetActive(true);

        // Gradually fade in
        float duration = 0.5f;
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / duration);
            SetLabelAlpha(alpha);
            yield return null;
        }

        // Wait for 2.5 second
        yield return new WaitForSeconds(2.5f);

        // Gradually fade out
        duration = 0.5f;
        startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / duration);
            SetLabelAlpha(alpha);
            yield return null;
        }

        // Hide the label after fading out
        notEnoughMoneyLabel.gameObject.SetActive(false);
    }

    private void SetLabelAlpha(float alpha)
    {
        Color labelColor = notEnoughMoneyLabel.color;
        labelColor.a = alpha;
        notEnoughMoneyLabel.color = labelColor;
    }

    private void ShakeCoinLabels()
    {
        foreach (ShakeEffect shakeEffect in shakeEffects)
        {
            if (shakeEffect != null)
            {
                shakeEffect.Shake();
            }
        }
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
