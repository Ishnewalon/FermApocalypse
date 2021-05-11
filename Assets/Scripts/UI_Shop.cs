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

    private int slotOffset = 70;

    private bool create = true;

    private void Awake()
    {
        shopContainer = transform.Find("ShopContainer");
        shopTemplate = shopContainer.Find("ShopTemplate");

        var templatePosition = shopTemplate.GetComponent<RectTransform>().anchoredPosition;
        rectX = templatePosition.x;
        rectY = templatePosition.y;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (GameManager.Instance.PlayerInventory.GetItemList().Count > 2 && create)
        {
            CreateShopButton(GameManager.Instance.PlayerInventory.GetItemList());
            create = false;
        }
    }

    private void CreateShopButton(List<Item> shopItems)
    {
        int shopRow = 0;

        foreach (var item in shopItems)
        {
            var shopSlot = Instantiate(shopTemplate, shopContainer);
            shopSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(rectX, rectY - shopRow * slotOffset);

            shopSlot.Find("ItemSprite").GetComponent<Image>().sprite = item.GetSprite();
            shopSlot.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(item.itemType.ToString());
            shopSlot.Find("Cost").GetComponent<TextMeshProUGUI>().SetText(item.GetCost().ToString());
            
            shopSlot.gameObject.SetActive(true);
            shopRow++;
        }
    }
}





















