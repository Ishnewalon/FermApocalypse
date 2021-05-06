using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowingController : MonoBehaviour
{
    private int _currrentGrowthStage = 0;
    private Item.ItemType _plantType;
    private bool isPlantWatered;

    private void OnMouseDown()
    {
        if (GameManager.Instance._currentHeldItem.itemClass == Item.ItemClass.Tools)
        {
            if (GameManager.Instance._currentHeldItem.itemType == Item.ItemType.WaterBucket)
            {
                isPlantWatered = true;
                gameObject.transform.parent.GetComponent<TerrainManager>().TerrainWatered();
            }
            else if (GameManager.Instance._currentHeldItem.itemType == Item.ItemType.Scythe &&
                     _currrentGrowthStage == 3)
            {
                gameObject.transform.parent.GetComponent<TerrainManager>().PlantHasBeenHarvested();
                gameObject.SetActive(false);
                ItemWorld.SpawnItemWorld(new Vector3(transform.position.x,
                        transform.position.y + 1, transform.position.z),
                    new Item {itemType = _plantType, itemClass = Item.ItemClass.Seeds, amount = 1});
            }
        }
    }

    private void MakePlantGrow()
    {
        GetComponent<SpriteRenderer>().sprite =
            GameManager.Instance._plantSprites[_plantType][_currrentGrowthStage];
    }

    public PlantStateData getStateData()
    {
        return new PlantStateData(_currrentGrowthStage, _plantType, isPlantWatered);
    }

    public void setStateData(int plantGrowthStage, Item.ItemType plantType, bool isWatered)
    {
        this._currrentGrowthStage = plantGrowthStage;
        this._plantType = plantType;
        this.isPlantWatered = isWatered;
        MakePlantGrow();
    }
}
