using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KhabarDetailPopup : MonoBehaviour
{
    private int id;
    private Sprite image;

    private Action<int, bool> buyItemCallback;

    public void ShowDetails(Sprite image, int id, Action<int, bool> callback)
    {
        this.image = image;
        this.id = id;
        buyItemCallback = callback;
        Image targetImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        targetImage.sprite = image;
        //targetImage.SetNativeSize();
        gameObject.SetActive(true);
    }

    

    public void BuyItem()
    {
        int currentCoins = CoinManager.GetCoins();
        
        bool isBougt = false;
        isBougt = true;
        buyItemCallback(id, isBougt);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

}
