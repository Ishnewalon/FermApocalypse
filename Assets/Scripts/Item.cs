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
        Watermelon,
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

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Hoe: return ItemAssets.Instance.hoeSprite;
            case ItemType.Scythe: return ItemAssets.Instance.scytheSprite;
            case ItemType.WaterBucket: return ItemAssets.Instance.WaterBucketSprite;
            default: return ItemAssets.Instance.hoeSprite;
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
            
            case ItemType.Watermelon : return new Color(255, 10, 10);
            
            case ItemType.WatermelonSeed : return new Color(255, 255, 255);
            case ItemType.EggplantSeed : return new Color(255, 255, 255);
        }
    }
}
