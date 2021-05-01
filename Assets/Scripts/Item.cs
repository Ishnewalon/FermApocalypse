using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{

    public enum ItemType
    {
        Hoe,
        Scythe,
        WaterBucket,
        
        Watermelon,
        //seeds
        BeanSeed,
        BlueberrySeed,
        CabbageSeed,
        CarrotSeed,
        CauliflowerSeed,
        CelerySeed,
        CornSeed,
        EggplantSeed,
        GrapeSeed,
        LeakSeed,
        OnionSeed,
        PepperSeed,
        PineappleSeed,
        PumpkinSeed,
        RockmelonSeed,
        SquashSeed,
        StrawberrySeed,
        TomatoSeed,        
        TurnipSeed,
        WatermelonSeed
    }

    public enum ItemClass
    {
        Tools,
        Produce,
        Seeds
    }

    public ItemType itemType;
    public ItemClass itemClass;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Hoe: return ItemAssets.Instance.hoeSprite;
            case ItemType.Scythe: return ItemAssets.Instance.scytheSprite;
            case ItemType.WaterBucket: return ItemAssets.Instance.waterBucketSprite;
            default: return ItemAssets.Instance.hoeSprite;
        }
    }

    public Color GetColor()
    {
        switch (itemClass)
        {
            case ItemClass.Tools : return Color.yellow;
            case ItemClass.Produce : return Color.magenta;
            case ItemClass.Seeds : return Color.green;
            default: return Color.yellow;
        }
    }

    public bool IsStackable()
    {
        switch (itemClass)
        {
            case ItemClass.Seeds:
            case ItemClass.Produce:
                return true;
            default:
                return false;
        }
    }
}
