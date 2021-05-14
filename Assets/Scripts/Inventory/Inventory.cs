using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{

    public class CoinBalanceChanged : UnityEvent<int>
    {
    }

    public UnityEvent OnItemListChanged;

    public CoinBalanceChanged OnCoinBalanceChanged;

    public Boolean isFull;
    
    private List<Item> itemList;

    private int coinBalance;

    public Inventory()
    {
        itemList = new List<Item>();
        OnItemListChanged = new UnityEvent();
        OnCoinBalanceChanged = new CoinBalanceChanged();
        coinBalance = 10000;
        
        AddItem(new Item { itemType = Item.ItemType.Houe, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Faux, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Seau, itemClass = Item.ItemClass.Tools, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Carotte, itemClass = Item.ItemClass.Seeds, amount = 10 });
    }

    public void ReplaceOrAddItem(Item newItem)
    {
        var originalItem = FindItemToReplace(newItem);
        
        if (originalItem == null)
        {
            AddItem(newItem);
            return;
        }
        
        var index = itemList.FindIndex(item =>
            item.itemType == originalItem.itemType && item.itemClass == originalItem.itemClass);
        
        itemList.RemoveAt(index);
        itemList.Insert(index, newItem);
        OnItemListChanged?.Invoke();
    }

    public Item FindItemToReplace(Item newItem)
    {
        String itemName = "";
        if (newItem.itemType.ToString().Contains("Houe"))
        {
            itemName = "Houe";
        }
        if (newItem.itemType.ToString().Contains("Faux"))
        {
            itemName = "Faux";
        }
        if (newItem.itemType.ToString().Contains("Seau"))
        {
            itemName = "Seau";
        }
        
        foreach (var item in itemList)
        {
            if (item.itemClass == Item.ItemClass.Tools && item.itemType.ToString().Contains(itemName))
            {
                return item;
            }
        }

        return null;
    }

    public void AddCoins(int amount)
    {
        coinBalance += amount;
        OnCoinBalanceChanged?.Invoke(coinBalance);
    }

    public bool RemoveCoins(int amount)
    {
        if (coinBalance - amount >= 0)
        {
            coinBalance -= amount;
            OnCoinBalanceChanged?.Invoke(coinBalance);
            return true;
        }

        return false;
    }

    public int GetBalance()
    {
        return coinBalance;
    }

    public int SellAllProduce()
    {
        int montant = 0;
        List<Item> tmpList = new List<Item>();
        foreach (var item in itemList)
        {
            if (item.itemClass == Item.ItemClass.Produce)
            {
                montant += item.GetCost() * item.amount;
            }
            else
            {
                tmpList.Add(item);
            }
        }

        itemList = tmpList;
        OnItemListChanged?.Invoke();

        AddCoins(montant);
        return montant;
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
        OnItemListChanged?.Invoke();
    }

    public void DropItem(Item item, Vector3 position)
    {
        if (itemList.Remove(item))
        {
            ItemWorld.SpawnItemWorld(new Vector3(position.x + 1, position.y, position.z), item);
            Debug.Log(itemList.Count);
        }
        OnItemListChanged?.Invoke();
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
                    inventoryItem.amount++;
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
        
        OnItemListChanged?.Invoke();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
