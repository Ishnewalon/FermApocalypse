using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public Item item;

    public bool isOwned;

    public ShopItem(Item item, bool isOwned)
    {
        this.item = item;
        this.isOwned = isOwned;
    }
}
