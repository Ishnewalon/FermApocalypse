using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{

    public ItemType itemType;
    public ItemClass itemClass;
    public int amount;

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
    
    public bool IsEquipable()
    {
        switch (itemClass)
        {
            case ItemClass.Seeds:
            case ItemClass.Tools:
                return true;
            default:
                return false;
        }
    }
    
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Hoe: return ItemAssets.Instance.hoeSprite;
            case ItemType.Scythe: return ItemAssets.Instance.scytheSprite;
            case ItemType.WaterBucket: return ItemAssets.Instance.waterBucketSprite;
            
            case ItemType.Bean: return ItemAssets.Instance.bean;
            case ItemType.Blueberry: return ItemAssets.Instance.blueberry;
            case ItemType.Cabbage: return ItemAssets.Instance.cabbage;
            case ItemType.Carrot: return ItemAssets.Instance.carrot;
            case ItemType.Cauliflower: return ItemAssets.Instance.cauliflower;
            case ItemType.Celery: return ItemAssets.Instance.celery;
            case ItemType.Corn: return ItemAssets.Instance.corn;
            case ItemType.Eggplant: return ItemAssets.Instance.eggplant;
            case ItemType.Grape: return ItemAssets.Instance.grape;
            case ItemType.Leak: return ItemAssets.Instance.leak;
            case ItemType.Onion: return ItemAssets.Instance.onion;
            case ItemType.Pepper: return ItemAssets.Instance.pepper;
            case ItemType.Pineapple: return ItemAssets.Instance.pineapple;
            case ItemType.Pumpkin: return ItemAssets.Instance.pumpkin;
            case ItemType.Rockmelon: return ItemAssets.Instance.rockmelon;
            case ItemType.Squash: return ItemAssets.Instance.squash;
            case ItemType.Strawberry: return ItemAssets.Instance.strawberry;
            case ItemType.Tomato: return ItemAssets.Instance.tomato;
            case ItemType.Turnip: return ItemAssets.Instance.turnip;
            case ItemType.Watermelon: return ItemAssets.Instance.watermelon;
            
            case ItemType.BeanSeed: return ItemAssets.Instance.bean;
            case ItemType.BlueberrySeed: return ItemAssets.Instance.blueberry;
            case ItemType.CabbageSeed: return ItemAssets.Instance.cabbage;
            case ItemType.CarrotSeed: return ItemAssets.Instance.carrot;
            case ItemType.CauliflowerSeed: return ItemAssets.Instance.cauliflower;
            case ItemType.CelerySeed: return ItemAssets.Instance.celery;
            case ItemType.CornSeed: return ItemAssets.Instance.corn;
            case ItemType.EggplantSeed: return ItemAssets.Instance.eggplant;
            case ItemType.GrapeSeed: return ItemAssets.Instance.grape;
            case ItemType.LeakSeed: return ItemAssets.Instance.leak;
            case ItemType.OnionSeed: return ItemAssets.Instance.onion;
            case ItemType.PepperSeed: return ItemAssets.Instance.pepper;
            case ItemType.PineappleSeed: return ItemAssets.Instance.pineapple;
            case ItemType.PumpkinSeed: return ItemAssets.Instance.pumpkin;
            case ItemType.RockmelonSeed: return ItemAssets.Instance.rockmelon;
            case ItemType.SquashSeed: return ItemAssets.Instance.squash;
            case ItemType.StrawberrySeed: return ItemAssets.Instance.strawberry;
            case ItemType.TomatoSeed: return ItemAssets.Instance.tomato;
            case ItemType.TurnipSeed: return ItemAssets.Instance.turnip;
            case ItemType.WatermelonSeed: return ItemAssets.Instance.watermelon;
            default: return ItemAssets.Instance.hoeSprite;
        }
    }
    
    public enum ItemClass
    {
        Tools,
        Produce,
        Seeds
    }
    
    public enum ItemType
    {
        EmptyHand,
        Hoe,
        Scythe,
        WaterBucket,
        //produce
        Bean,
        Blueberry,
        Cabbage,
        Carrot,
        Cauliflower,
        Celery,
        Corn,
        Eggplant,
        Grape,
        Leak,
        Onion,
        Pepper,
        Pineapple,
        Pumpkin,
        Rockmelon,
        Squash,
        Strawberry,
        Tomato,
        Turnip,
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
}
