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
            case ItemType.Hoe : return Color.yellow;
            case ItemType.Scythe : return Color.yellow;
            case ItemType.WaterBucket : return Color.yellow;
            
            case ItemType.Watermelon : return Color.red;
            
            case ItemType.WatermelonSeed : return Color.green;
            case ItemType.EggplantSeed : return Color.green;
            default: return Color.yellow;
        }
    }
}
