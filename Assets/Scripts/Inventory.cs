using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        
        AddItem(new Item { itemType = Item.ItemType.Hoe, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Scythe, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.WateringCan, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Plant, amount = 30 });
        Debug.Log("inventory");
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
