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

    public String GetDesc()
    {
        switch (itemType)
        {
            case ItemType.Hoe: return "Une houe pour labourer la terre";
            case ItemType.Scythe: return "Une faux pour faire la recolte";
            case ItemType.WaterBucket: return "Un seau d'eau"; //todo add desc for new items!!!!!!!!!!!
        }
        switch (itemClass)
        {
            case ItemClass.Seeds: return "Des semences de " + itemType;
            case ItemClass.Produce: return "Le fruit de vos efforts";
            default: return "Where did you find this?";
        }
    }

    public int GetCost()
    {
        if (itemClass == ItemClass.Produce)
        {
            return 10;
        }
        if (itemClass == ItemClass.Seeds)
        {
            return 2;
        }
        if (itemClass == ItemClass.Tools)
        {
            switch (itemType)
            {
                case ItemType.Scythe: return 10;
                case ItemType.Hoe: return 10;
                case ItemType.WaterBucket: return 10;
                case ItemType.GoldenHoe: return 300;
                case ItemType.GoldenScythe: return 300;
                case ItemType.GoldenBucket: return 300;
                case ItemType.CandyHoe: return 1000;
                case ItemType.LimeScythe: return 1000;
                case ItemType.FireBucket: return 1000;
            }
        }
        return 0;
    }

    public Texture2D GetTexture2D()
    {
        switch (itemType)
        {
            case ItemType.Hoe: return ItemAssets.Instance.basicHoe;
            case ItemType.Scythe: return ItemAssets.Instance.basicScythe;
            case ItemType.WaterBucket: return ItemAssets.Instance.basicBucket;
            case ItemType.GoldenHoe: return ItemAssets.Instance.goldenHoe;
            case ItemType.GoldenScythe: return ItemAssets.Instance.goldenScythe;
            case ItemType.GoldenBucket: return ItemAssets.Instance.goldenBucket;
            case ItemType.CandyHoe: return ItemAssets.Instance.candyHoe;
            case ItemType.LimeScythe: return ItemAssets.Instance.limeScythe;
            case ItemType.FireBucket: return ItemAssets.Instance.fireBucket;
        }

        return null;
    }
    
    public Sprite GetSprite()
    {
        if (itemClass == ItemClass.Produce)
        {
            switch (itemType)
            {
                case ItemType.Haricot: return ItemAssets.Instance.bean;
                case ItemType.Bleuet: return ItemAssets.Instance.blueberry;
                case ItemType.Chou: return ItemAssets.Instance.cabbage;
                case ItemType.Carotte: return ItemAssets.Instance.carrot;
                case ItemType.Choufleur: return ItemAssets.Instance.cauliflower;
                case ItemType.Celeri: return ItemAssets.Instance.celery;
                case ItemType.Mais: return ItemAssets.Instance.corn;
                case ItemType.Aubergine: return ItemAssets.Instance.eggplant;
                case ItemType.Raisin: return ItemAssets.Instance.grape;
                case ItemType.Poireau: return ItemAssets.Instance.leak;
                case ItemType.Oignon: return ItemAssets.Instance.onion;
                case ItemType.Poivron: return ItemAssets.Instance.pepper;
                case ItemType.Ananas: return ItemAssets.Instance.pineapple;
                case ItemType.Citrouille: return ItemAssets.Instance.pumpkin;
                case ItemType.Rockmelon: return ItemAssets.Instance.rockmelon;
                case ItemType.Courge: return ItemAssets.Instance.squash;
                case ItemType.Fraise: return ItemAssets.Instance.strawberry;
                case ItemType.Tomate: return ItemAssets.Instance.tomato;
                case ItemType.Navet: return ItemAssets.Instance.turnip;
                case ItemType.Pasteque: return ItemAssets.Instance.watermelon;
            }
        }
        if (itemClass == ItemClass.Seeds)
        {
            switch (itemType)
            {
                case ItemType.Haricot: return ItemAssets.Instance.beanSeeds;
                case ItemType.Bleuet: return ItemAssets.Instance.blueberrySeeds;
                case ItemType.Chou: return ItemAssets.Instance.cabbageSeeds;
                case ItemType.Carotte: return ItemAssets.Instance.carrotSeeds;
                case ItemType.Choufleur: return ItemAssets.Instance.cauliflowerSeeds;
                case ItemType.Celeri: return ItemAssets.Instance.celerySeeds;
                case ItemType.Mais: return ItemAssets.Instance.cornSeeds;
                case ItemType.Aubergine: return ItemAssets.Instance.eggplantSeeds;
                case ItemType.Raisin: return ItemAssets.Instance.grapeSeeds;
                case ItemType.Poireau: return ItemAssets.Instance.leakSeeds;
                case ItemType.Oignon: return ItemAssets.Instance.onionSeeds;
                case ItemType.Poivron: return ItemAssets.Instance.pepperSeeds;
                case ItemType.Ananas: return ItemAssets.Instance.pineappleSeeds;
                case ItemType.Citrouille: return ItemAssets.Instance.pumpkinSeeds;
                case ItemType.Rockmelon: return ItemAssets.Instance.rockmelonSeeds;
                case ItemType.Courge: return ItemAssets.Instance.squashSeeds;
                case ItemType.Fraise: return ItemAssets.Instance.strawberrySeeds;
                case ItemType.Tomate: return ItemAssets.Instance.tomatoSeeds;
                case ItemType.Navet: return ItemAssets.Instance.turnipSeeds;
                case ItemType.Pasteque: return ItemAssets.Instance.watermelonSeeds;
            }
        }
        if (itemClass == ItemClass.Tools)
        {
            switch (itemType)
            {
                case ItemType.Scythe: return ItemAssets.Instance.scytheSprite;
                case ItemType.Hoe: return ItemAssets.Instance.hoeSprite;
                case ItemType.WaterBucket: return ItemAssets.Instance.waterBucketSprite;
                case ItemType.GoldenHoe: return ItemAssets.Instance.GoldenHoeSprite;
                case ItemType.GoldenScythe: return ItemAssets.Instance.GoldenScytheSprite;
                case ItemType.GoldenBucket: return ItemAssets.Instance.GoldenBucketSprite;
                case ItemType.CandyHoe: return ItemAssets.Instance.CandyHoeSprite;
                case ItemType.LimeScythe: return ItemAssets.Instance.LimeScytheSprite;
                case ItemType.FireBucket: return ItemAssets.Instance.FireBucketSprite;
            }
        }
        return ItemAssets.Instance.bean;
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
        GoldenHoe,
        GoldenScythe,
        GoldenBucket,
        CandyHoe,
        LimeScythe,
        FireBucket,
        
        Haricot,
        Bleuet,
        Chou,
        Carotte,
        Choufleur,
        Celeri,
        Mais,
        Aubergine,
        Raisin,
        Poireau,
        Oignon,
        Poivron,
        Ananas,
        Citrouille,
        Rockmelon,
        Courge,
        Fraise,
        Tomate,
        Navet,
        Pasteque,
    }
}
