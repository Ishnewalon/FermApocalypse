using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolVendor : MonoBehaviour
{
    
    private List<ShopItem> shopList;

    private void Start()
    {
        shopList = new List<ShopItem>();
        
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Houe, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Faux, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.Seau, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.HoueOr, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.FauxOr, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.SeauOr, itemClass = Item.ItemClass.Tools, amount = 1},
            false));

        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.HoueBonbon, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.FauxLime, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
        shopList.Add(new ShopItem(new Item {itemType = Item.ItemType.SeauFeu, itemClass = Item.ItemClass.Tools, amount = 1},
            false));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.UIShop.CreateShopButton(shopList, "Vendeur d'outils");
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        GameManager.Instance.UIShop.ClearShopButtons();
    }
}
