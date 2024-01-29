using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceFormatter : MonoBehaviour
{

    public string FormatPrice(int price)
    {
        if (price >= 1000000000)
        {
            return (price % 1000000000 == 0 ? (price / 1000000000).ToString("F0") : (price / 1000000000f).ToString("F1")) + "B";
        }
        else if (price >= 1000000)
        {
            return (price % 1000000 == 0 ? (price / 1000000).ToString("F0") : (price / 1000000f).ToString("F1")) + "M";
        }
        else if (price >= 10000)
        {
            return (price % 1000 == 0 ? (price / 1000).ToString("F0") : (price / 1000f).ToString("F1")) + "K";
        }
        else
        {
            return price.ToString("F0");
        }
    }
}

