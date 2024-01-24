using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform scrollview;
    public List<ShopItem> ShopItemsList;
    public KhabarDetailPopup khabarDetailPopup;

    [System.Serializable]
    public class ShopItem
    {
        public int id;
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
        public string descriptionText;
    }

    Button buyBtn;

    void Start()
    {
        int len = ShopItemsList.Count;
        itemTemplate = scrollview.GetChild(0).gameObject;
        for (int i =0; i < len; i++)
        {
            
            g = Instantiate(itemTemplate, scrollview);
            Image imageComponent = g.transform.GetChild(0).GetComponent<Image>();
            imageComponent.sprite = ShopItemsList[i].Image;
            Color currentColor = imageComponent.color;
            imageComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
            g.transform.GetChild(1).GetComponent<TMP_Text>().text = ShopItemsList[i].Price.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();

            EventTrigger trigger = g.AddComponent<EventTrigger>();

            ShopItemsList[i].id = i;
            int currentItemID = ShopItemsList[i].id;

            Sprite currentImage = ShopItemsList[i].Image;
            GameObject currentGameObject = g;

            int currentPrice = ShopItemsList[i].Price;
            string descriptionText = ShopItemsList[i].descriptionText;
            bool isBought = ShopItemsList[i].IsPurchased;

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => {
                OnPanelClick(currentImage, currentItemID, currentPrice, descriptionText, isBought, currentGameObject);
            });
            trigger.triggers.Add(entry);

            Image imageLock = g.transform.GetChild(3).GetComponent<Image>();
            if (ShopItemsList[i].IsPurchased)
            {
                imageLock.gameObject.SetActive(false);
            }
            else
            {
                imageLock.gameObject.SetActive(true);
            }
        }
        Destroy(itemTemplate);
    }

    void OnPanelClick(Sprite image, int id, int price, string descriptionText, bool isBought, GameObject g)
    {

        khabarDetailPopup.ShowDetails(image, id, price, descriptionText, isBought, (itemID, isBought) =>
        {
            ShopItem itemToBuy = ShopItemsList.Find(item => item.id == itemID);
            itemToBuy.IsPurchased = isBought;
            g.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(!isBought);
        });
    }
}