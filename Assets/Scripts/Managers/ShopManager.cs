using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using System.IO;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform scrollview;
    public ShopItem[] ShopItemsList;
    public KhabarDetailPopup khabarDetailPopup;
    public TMP_Text currentCoins;

    private GameObject itemTemplate;

    private void Update()
    {
        currentCoins.text = CoinManager.GetCoins().ToString();
    }

    void Start()
    {
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
        LoadShopData();
        InitializeShop();
        SaveShopData();
    }

    void SaveShopData()
    {
        List<ShopItemModel> shopItemModels = new List<ShopItemModel>();

        foreach (ShopItem shopItem in ShopItemsList)
        {
            ShopItemModel model = new ShopItemModel
            {
                id = shopItem.id,
                price = shopItem.price,
                title = shopItem.title,
                isPurchased = shopItem.isPurchased,
                descriptionText = shopItem.descriptionText
            };

            shopItemModels.Add(model);
        }

        ShopDataWrapper wrapper = new ShopDataWrapper(shopItemModels);

        string json = JsonUtility.ToJson(wrapper);

        File.WriteAllText(Application.persistentDataPath + "/shopdata.json", json);
    }

    void LoadShopData()
    {
        string path = Application.persistentDataPath + "/shopdata.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            List<ShopItemModel> shopItemModels = JsonUtility.FromJson<List<ShopItemModel>>(json);

            for (int i = 0; i < shopItemModels.Count && i < ShopItemsList.Length; i++)
            {
                ShopItemModel model = shopItemModels[i];
                ShopItem shopItem = ShopItemsList[i];

                // Update only relevant properties
                shopItem.isPurchased = model.isPurchased;
            }
        }
    }

    void InitializeShop()
    {
        int len = ShopItemsList.Length;
        itemTemplate = scrollview.GetChild(0).gameObject;

        for (int i = 0; i < len; i++)
        {
            GameObject newItem = InstantiateItem();
            SetupItemUI(newItem, i);
            SetupEventTrigger(newItem, ShopItemsList[i]);
            SetupLockImage(newItem, i);
        }
        
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
        if (ShopItemsList[index].isPurchased)
        {
            SetImageAlpha(imageComponent, 1f);
        } else
        {
            SetImageAlpha(imageComponent, 0.5f);
        }

        item.transform.GetChild(1).GetComponent<TMP_Text>().text = ShopItemsList[index].title.ToString();
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
        imageLock.gameObject.SetActive(!ShopItemsList[index].isPurchased);
    }

    void SetImageAlpha(Image image, float alpha)
    {
        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
    }

    void OnPanelClick(ShopItem shopItem, GameObject item)
    {
        Button backbtn = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
        backbtn.gameObject.SetActive(false);
        khabarDetailPopup.ShowDetails(shopItem, (itemID, isBought) =>
        {
            shopItem.isPurchased = isBought;
            item.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(!isBought);

            SaveShopData();
            Image imageComponent = item.transform.GetChild(0).GetComponent<Image>();
            imageComponent.sprite = shopItem.Image;
            SetImageAlpha(imageComponent, 1f);
        },
        isClosed =>
        {
            backbtn.gameObject.SetActive(true);
        });
    }
   
}

[System.Serializable]
public class ShopDataWrapper
{
    public ShopItemModel[] shopItems;

    public ShopDataWrapper(List<ShopItemModel> itemList)
    {
        shopItems = itemList.ToArray();
    }
}