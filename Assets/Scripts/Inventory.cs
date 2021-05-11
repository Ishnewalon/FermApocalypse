using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;

    public Boolean isFull = false;
    
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        
        AddItem(new Item { itemType = Item.ItemType.Hoe, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Scythe, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.WaterBucket, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Ananas, itemClass = Item.ItemClass.Seeds, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.Citrouille, itemClass = Item.ItemClass.Seeds, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.Raisin, itemClass = Item.ItemClass.Seeds, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.Fraise, itemClass = Item.ItemClass.Seeds, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.Ananas, itemClass = Item.ItemClass.Produce, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.Citrouille, itemClass = Item.ItemClass.Produce, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.Raisin, itemClass = Item.ItemClass.Produce, amount = 5 });
    }

    public void UseItem(Item searchItem)
    {
        var usedItem = itemList.Find(item =>
            item.itemType == searchItem.itemType && item.itemClass == searchItem.itemClass);
        usedItem.amount--;
        if (usedItem.amount < 1)
        {
            GameManager.Instance._currentHeldItem = new Item { itemType = Item.ItemType.EmptyHand, itemClass = Item.ItemClass.Tools, amount = 1 };
            itemList.Remove(usedItem);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DropItem(Item item, Vector3 position)
    {
        if (itemList.Remove(item))
        {
            ItemWorld.SpawnItemWorld(new Vector3(position.x + 1, position.y, position.z), item);
            Debug.Log(itemList.Count);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddItem(Item item)
    {
        if (isFull)
        {
            return;
        }
        
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType && inventoryItem.itemClass == item.itemClass)
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

        if (itemList.Count > 17)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
