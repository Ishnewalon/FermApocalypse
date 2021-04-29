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
        WaterMelonSeed,
        EggPlantSeed
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default: return ItemAssets.Instance.hoeSprite;
            case ItemType.Hoe: return ItemAssets.Instance.hoeSprite;
            case ItemType.Scythe: return ItemAssets.Instance.scytheSprite;
            case ItemType.WaterBucket: return ItemAssets.Instance.WaterBucketSprite;
        }
    }
}
