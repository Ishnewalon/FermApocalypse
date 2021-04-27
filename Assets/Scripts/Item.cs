using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Hoe,
        Scythe,
        WaterBucket,
        WaterMelon,
        WaterMelonSeed
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default: 
            case ItemType.Hoe: return ItemAssets.Instance.hoeSprite;
            case ItemType.Scythe: return ItemAssets.Instance.scytheSprite;
            case ItemType.WaterBucket: return ItemAssets.Instance.WaterBucketSprite;
        }
    }
}
