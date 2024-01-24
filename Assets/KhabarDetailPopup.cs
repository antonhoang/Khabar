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

    public void ShowDetails(Sprite image, int id)
    {
        this.image = image;
        this.id = id;

        Image targetImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        
        targetImage.sprite = image;

        gameObject.SetActive(true);
    }

    void BuyItem(Action<int, bool> buyItemCallback)
    {
        // check for money
        bool isBougt = false;
        isBougt = true;
        buyItemCallback(id, isBougt);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

}
