using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStateData
{
    public bool isPlanted;
    public int plantGrowthStage;
    public Item.ItemType plantType;
    public bool isWatered;

    public PlantStateData()
    {
        this.isPlanted = false;
    }

    public PlantStateData(int plantGrowthStage, Item.ItemType plantType, bool isWatered)
    {
        this.isPlanted = true;
        this.plantGrowthStage = plantGrowthStage;
        this.plantType = plantType;
        this.isWatered = isWatered;
    }
}
