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
        
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Ananas, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Aubergine, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Bleuet, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Carotte, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Celeri, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Chou, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Choufleur, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Citrouille, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Courge, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Fraise, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Haricot, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Mais, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Navet, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Oignon, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Pasteque, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Poireau, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Poivron, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Raisin, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Rockmelon, itemClass = Item.ItemClass.Seeds, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Tomate, itemClass = Item.ItemClass.Seeds, amount = 1},
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
