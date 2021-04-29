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
            default: 
            case ItemType.Hoe: return ItemAssets.Instance.hoeSprite;
            case ItemType.Scythe: return ItemAssets.Instance.scytheSprite;
            case ItemType.WaterBucket: return ItemAssets.Instance.WaterBucketSprite;
        }
    }

    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Hoe : return new Color(150, 100, 10);
            case ItemType.Scythe : return new Color(150, 100, 10);
            case ItemType.WaterBucket : return new Color(150, 100, 10);
            
            case ItemType.WaterMelon : return new Color(255, 10, 10);
            
            case ItemType.WaterMelonSeed : return new Color(255, 255, 255);
            case ItemType.EggPlantSeed : return new Color(255, 255, 255);
        }
    }
}
