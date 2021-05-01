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
        
        AddItem(new Item { itemType = Item.ItemType.Hoe, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Scythe, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.WaterBucket, itemClass = Item.ItemClass.Tools, amount = 1 });
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty); // question mark to avoid possible null pointer
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
