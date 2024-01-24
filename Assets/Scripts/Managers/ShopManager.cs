using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform scrollview;

    void Start()
    {
        itemTemplate = scrollview.GetChild(0).gameObject;
        for (int i =0; i < 20; i++)
        {
            g = Instantiate(itemTemplate, scrollview);
        }
        Destroy(itemTemplate);
    }
}