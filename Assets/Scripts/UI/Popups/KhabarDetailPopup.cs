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

    public Image notEnoughMoneyView;

    public ParticleSystem BuyCoinEffect;

    public GameObject supportUs;
    public GameObject supportUA;

    public List<GameObject> coinLabels;

    private List<ShakeEffect> shakeEffects = new List<ShakeEffect>();

    private void Awake()
    {
        var mainModule = BuyCoinEffect.main;
        mainModule.playOnAwake = false;
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

    private void Start()
    {
        BuyCoinEffect.Stop();
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
        notEnoughMoneyView.gameObject.SetActive(false);
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
        GameObject coinLabel = GetLastItem(coinLabels);

        if (!shopItem.isPurchased)
        {
            coinLabel.gameObject.SetActive(true);


            Image im1 = coinLabel.transform.GetChild(0).GetComponent<Image>();
            Image im2 = coinLabel.transform.GetChild(1).GetComponent<Image>();

            TMP_Text t1 = im1.transform.GetChild(0).GetComponent<TMP_Text>();
            Color imageColor1 = im1.color;
            Color imageColor2 = im2.color;

            Color textColor1 = t1.color;

            imageColor1.a = 1;
            imageColor2.a = 1;

            textColor1.a = 1;

            im1.color = imageColor1;
            im2.color = imageColor2;
            t1.color = textColor1;

            PriceFormatter priceFormatter = new PriceFormatter();
            t1.text = priceFormatter.FormatPrice(shopItem.price);
        }
        else
        {
            Image im1 = coinLabel.transform.GetChild(0).GetComponent<Image>();
            Image im2 = coinLabel.transform.GetChild(1).GetComponent<Image>();

            TMP_Text t1 = im1.transform.GetChild(0).GetComponent<TMP_Text>();
            Color imageColor1 = im1.color;
            Color imageColor2 = im2.color;

            Color textColor1 = t1.color;

            imageColor1.a = 0;
            imageColor2.a = 0;

            textColor1.a = 0;

            im1.color = imageColor1;
            im2.color = imageColor2;
            t1.color = textColor1;
            coinLabel.gameObject.SetActive(false);
        }
    }

    public void BuyItem()
    {
        int currentCoins = CoinManager.GetCoins();

        if (currentCoins > price && currentCoins > 0)
        {
            CoinManager.RemoveCoins(price);
            isBought = true;
            buyItemCallback(id, isBought);
            BuyCoinEffect.Stop();
            BuyCoinEffect.Play();
            UpdateBuyButton();
            StartCoroutine(FadeCoinLabel());
        } else
        {
            StartCoroutine(ShowNotEnoughMoneyView());
            ShakeCoinLabels();
        }
    } 

    private IEnumerator FadeCoinLabel()
    {
        yield return new WaitForSeconds(3f);
        GameObject coinLabel = GetLastItem(coinLabels);
        float duration = 0.5f;
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / duration);
            

            Image im1 = coinLabel.transform.GetChild(0).GetComponent<Image>();
            Image im2 = coinLabel.transform.GetChild(1).GetComponent<Image>();

            TMP_Text t1 = im1.transform.GetChild(0).GetComponent<TMP_Text>();
            Color imageColor1 = im1.color;
            Color imageColor2 = im2.color;

            Color textColor1 = t1.color;
            
            imageColor1.a = alpha;
            imageColor2.a = alpha;

            textColor1.a = alpha;

            im1.color = imageColor1;
            im2.color = imageColor2;
            t1.color = textColor1;
            

            yield return null;
        }
        coinLabel.gameObject.SetActive(false);
    }

    private T GetLastItem<T>(List<T> list)
    {
        if (list.Count > 0)
        {
            return list[list.Count - 1];
        }
        else
        {
            // Handle the case where the list is empty
            Debug.LogError("List is empty");
            return default(T); // Returns the default value for type T (null for reference types, 0 for value types)
        }
    }

    private System.Collections.IEnumerator ShowNotEnoughMoneyView()
    {
        // Display the label indicating not enough money
        notEnoughMoneyView.gameObject.SetActive(true);

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
        
        notEnoughMoneyView.gameObject.SetActive(false);
    }

    private void SetLabelAlpha(float alpha)
    {
        TMP_Text label = notEnoughMoneyView.GetComponentInChildren<TMP_Text>();
        Color imageColor = notEnoughMoneyView.color;
        Color labelColor = label.color;
        imageColor.a = alpha;
        labelColor.a = alpha;
        notEnoughMoneyView.color = imageColor;
        label.color = labelColor;
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
