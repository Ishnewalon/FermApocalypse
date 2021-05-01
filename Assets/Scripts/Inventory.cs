using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;
    
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        
        AddItem(new Item { itemType = Item.ItemType.Hoe, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Scythe, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.WaterBucket, amount = 1 });
        Debug.Log("inventory");
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty); // question mark to avoid possible null pointer
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
