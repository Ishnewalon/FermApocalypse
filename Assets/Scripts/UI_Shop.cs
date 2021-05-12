using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Transform shopContainer;

    private Transform shopTemplate;

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

        var templatePosition = shopTemplate.GetComponent<RectTransform>().anchoredPosition;
        rectX = templatePosition.x;
        rectY = templatePosition.y;
    }

    public void CreateShopButton(List<ShopItem> shopItems)
    {
        _gameObject.SetActive(true);
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

            shopSlot.gameObject.SetActive(true);
            shopRow++;
        }
    }

    public void ClearShopButtons()
    {
        foreach (Transform child in shopContainer.transform)
        {
            if (child != shopTemplate)
            {
                Destroy(child.gameObject);
            }
        }
    }
}





















