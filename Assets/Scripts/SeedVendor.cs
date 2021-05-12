using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedVendor : MonoBehaviour
{

    private List<ShopItem> shopList;

    private void Start()
    {
        shopList = new List<ShopItem>();
        
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Hoe, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Scythe, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.WaterBucket, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.GoldenHoe, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.GoldenScythe, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.GoldenBucket, itemClass = Item.ItemClass.Tools, amount = 1},
            false));

        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.CandyHoe, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.LimeScythe, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.FireBucket, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.UIShop.CreateShopButton(shopList);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        GameManager.Instance.UIShop.ClearShopButtons();
    }
}
