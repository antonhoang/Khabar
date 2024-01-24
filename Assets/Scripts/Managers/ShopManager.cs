using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
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

    private GameObject itemTemplate;
    private const string PlayerPrefsKey = "ShopItemsList";


    void Start()
    {
        LoadShopItems();
        InitializeShop();
    }

    void SaveShopItems()
    {
        string jsonString = JsonUtility.ToJson(ShopItemsList);
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    void LoadShopItems()
    {
        // Load the serialized ShopItemsList from PlayerPrefs
        string jsonString = PlayerPrefs.GetString(PlayerPrefsKey, "");
        if (!string.IsNullOrEmpty(jsonString) && jsonString != "{}")
        {
            ShopItemsList = JsonUtility.FromJson<List<ShopItem>>(jsonString);
        }
    }

    void InitializeShop()
    {
        int len = ShopItemsList.Count;
        itemTemplate = scrollview.GetChild(0).gameObject;

        for (int i = 0; i < len; i++)
        {
            GameObject newItem = InstantiateItem();
            SetupItemUI(newItem, i);
            SetupEventTrigger(newItem, ShopItemsList[i]);
            SetupLockImage(newItem, i);
        }
        SaveShopItems();
        Destroy(itemTemplate);
    }

    GameObject InstantiateItem()
    {
        return Instantiate(itemTemplate, scrollview);
    }

    void SetupItemUI(GameObject item, int index)
    {
        Image imageComponent = item.transform.GetChild(0).GetComponent<Image>();
        imageComponent.sprite = ShopItemsList[index].Image;
        SetImageAlpha(imageComponent, 0.5f);

        item.transform.GetChild(1).GetComponent<TMP_Text>().text = ShopItemsList[index].Price.ToString();

        ShopItemsList[index].id = index;
    }

    void SetupEventTrigger(GameObject item, ShopItem shopItem)
    {
        ShopItem localShopItem = shopItem;
        EventTrigger trigger = item.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) =>
        {
            OnPanelClick(localShopItem, item);
        });
        trigger.triggers.Add(entry);
    }

    void SetupLockImage(GameObject item, int index)
    {
        Image imageLock = item.transform.GetChild(3).GetComponent<Image>();
        imageLock.gameObject.SetActive(!ShopItemsList[index].IsPurchased);
    }

    void SetImageAlpha(Image image, float alpha)
    {
        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }

    void OnPanelClick(ShopItem shopItem, GameObject item)
    {
        khabarDetailPopup.ShowDetails(shopItem, (itemID, isBought) =>
        {
            shopItem.IsPurchased = isBought;
            item.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(!isBought);
            SaveShopItems();
        });
    }
   
}