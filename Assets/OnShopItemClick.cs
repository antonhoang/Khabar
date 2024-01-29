using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class OnShopItemClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<ShopItem, GameObject> OnItemClick;

    private ShopItem shopItem;

    private bool isPointerDown;
    private Vector2 pointerDownPosition;

    public void Initialize(ShopItem data)
    {
        shopItem = data;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        isPointerDown = true;
        pointerDownPosition = eventData.position;
        
        //OnItemClick.Invoke(shopItem, this.gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            float dragThreshold = 5f; // Adjust this threshold as needed

            // Check if the pointer has moved beyond the threshold
            float distance = Vector2.Distance(pointerDownPosition, eventData.position);
            if (distance < dragThreshold)
            {
                // Trigger the OnItemClick event for clicks
                OnItemClick?.Invoke(shopItem, this.gameObject);
            }
        }

        isPointerDown = false;

    }



}


