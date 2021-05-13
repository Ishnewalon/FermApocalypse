using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Transform shopContainer;

    private Transform shopTemplate;

    private Transform shopProduce;

    private TextMeshProUGUI shopName;

    private float rectX;

    private float rectY;

    private int slotOffset = 50;

    private bool create = true;

    private GameObject _gameObject;

    public void InitializeShop(GameObject gameObject)
    {
        _gameObject = gameObject;
        shopContainer = _gameObject.transform.Find("ShopContainer");
        shopTemplate = shopContainer.Find("ShopTemplate");
        shopProduce = _gameObject.transform.Find("ProduceBuyer");
        shopName = _gameObject.transform.Find("ShopName").GetComponent<TextMeshProUGUI>();
        
        

        var templatePosition = shopTemplate.GetComponent<RectTransform>().anchoredPosition;
        rectX = templatePosition.x;
        rectY = templatePosition.y;
    }

    public void CreateShopButton(List<ShopItem> shopItems, String name)
    {
        _gameObject.SetActive(true);
        shopName.SetText(name);
        int shopRow = 0;

        foreach (var shopItem in shopItems)
        {
            var shopSlot = Instantiate(shopTemplate, shopContainer);
            shopSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(rectX, rectY - shopRow * slotOffset);

            shopSlot.Find("ItemSprite").GetComponent<Image>().sprite = shopItem.item.GetSprite();
            shopSlot.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(shopItem.item.itemType.ToString());
            
            if (shopItem.isOwned)
            {
                shopSlot.Find("Cost").GetComponent<TextMeshProUGUI>().SetText("FREE");
            }
            else
            {
                shopSlot.Find("Cost").GetComponent<TextMeshProUGUI>().SetText(shopItem.item.GetCost().ToString());
            }
            
            AddEvent(shopSlot, EventTriggerType.PointerClick, delegate {OnPointerClick(shopItem);});
            //AddEvent(shopSlot, EventTriggerType.PointerEnter, delegate {OnPointerEnter(shopItem, shopSlot);});
            //AddEvent(shopSlot, EventTriggerType.PointerExit, delegate {OnPointerExit(shopItem, shopSlot);});

            shopSlot.gameObject.SetActive(true);
            shopRow++;
        }
    }

    public void ToggleSellShop(bool flag)
    {
        _gameObject.SetActive(flag);
        shopProduce.gameObject.SetActive(flag);
        shopProduce.Find("btn_vendre").GetComponent<Button>().onClick.AddListener(delegate { SellAllProduce(); });
        shopProduce.Find("montant").gameObject.SetActive(false);
        shopName.SetText("Acheteur");
    }

    public void SellAllProduce()
    {
        shopProduce.Find("montant").GetComponent<TextMeshProUGUI>().SetText("+"+GameManager.Instance.PlayerInventory.SellAllProduce()+"$");
        shopProduce.Find("montant").gameObject.SetActive(true);-
    }

    private void OnPointerExit(ShopItem shopItem, Transform slot)
    {
        throw new NotImplementedException();
    }

    private void OnPointerEnter(ShopItem shopItem, Transform slot)
    {
        throw new NotImplementedException();
    }

    private void OnPointerClick(ShopItem shopItem)
    {
        if (GameManager.Instance.PlayerInventory.isFull)
        {
            return;
        }
        if (GameManager.Instance.PlayerInventory.RemoveCoins(shopItem.item.GetCost()))
        {
            print(GameManager.Instance.PlayerInventory.GetBalance());
            if (shopItem.item.itemClass == Item.ItemClass.Tools)
            {
                GameManager.Instance.PlayerInventory.ReplaceOrAddItem(shopItem.item);
            }
            else
            {
                GameManager.Instance.PlayerInventory.AddItem(shopItem.item);
            }
        }
    }

    public void ClearShopButtons()
    {
        _gameObject.SetActive(false);
        foreach (Transform child in shopContainer.transform)
        {
            if (child != shopTemplate)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    private void AddEvent(Transform slotTransform, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = slotTransform.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener((data) => { action((PointerEventData)data);});
        trigger.triggers.Add(eventTrigger);
    }
}





















