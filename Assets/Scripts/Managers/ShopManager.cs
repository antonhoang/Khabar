using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform scrollview;
    public ShopItemList ShopItemsList;
    public KhabarDetailPopup khabarDetailPopup;
    public TMP_Text currentCoins;

    [System.Serializable]
    public class ShopItem
    {
        public int id;
        public Sprite Image;
        public int Price;
        public bool IsPurchased;
        public string descriptionText;
    }

    private GameObject itemTemplate;
    private Dictionary<int, bool> purchasedStatus = new Dictionary<int, bool>();

    private void Update()
    {
        currentCoins.text = CoinManager.GetCoins().ToString();
    }

    void Start()
    {
        InitializeShop();
    }

    void InitializeShop()
    {
        int len = ShopItemsList.items.Count;
        itemTemplate = scrollview.GetChild(0).gameObject;

        for (int i = 0; i < len; i++)
        {
            GameObject newItem = InstantiateItem();
            SetupItemUI(newItem, i);
            SetupEventTrigger(newItem, ShopItemsList.items[i]);
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
        imageComponent.sprite = ShopItemsList.items[index].Image;
        SetImageAlpha(imageComponent, 0.5f);

        item.transform.GetChild(1).GetComponent<TMP_Text>().text = ShopItemsList.items[index].Price.ToString();
        ShopItemsList.items[index].id = index;
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
        imageLock.gameObject.SetActive(!ShopItemsList.items[index].IsPurchased);
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
            shopItem.IsPurchased = isBought;
            item.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(!isBought);
        },
        isClosed =>
        {
            backbtn.gameObject.SetActive(true);
        });
    }
   
}