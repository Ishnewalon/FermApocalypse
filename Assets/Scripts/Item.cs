using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Hoe,
        Scythe,
        WateringCan,
        Plant,
        Coin
    }

    public ItemType itemType;
    public int amount;

}
