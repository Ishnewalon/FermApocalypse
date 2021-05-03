using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowingController : MonoBehaviour
{
    private int _currrentGrowthStage = 0;
    private Enum _plantType;
    private bool isPlantWatered;
    private const int _DaysNeededForMaxGrowth = 4;
    
    void Update()
    {
        if (gameObject.activeSelf == true && _currrentGrowthStage == 0 && GameManager.Instance._cropSeedEnum.Contains(GameManager.Instance._currentHeldItem))
        {
            _plantType = GameManager.Instance._currentHeldItem;
            PlantASeed();
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance._cropSeedEnum.Contains(GameManager.Instance._currentHeldItem))
        {
            if (GameManager.Instance._currentHeldItem.Equals(Item.ItemType.WaterBucket) && gameObject.activeSelf == true)
            {
                isPlantWatered = true;
                gameObject.transform.parent.GetComponent<TerrainManager>().TerrainWatered();
            }
        }
    }

    private void PlantASeed()
    {
        GetComponent<SpriteRenderer>().sprite =
            GameManager.Instance._plantSprites[_plantType][_currrentGrowthStage];
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

    public void setStateData(int plantGrowthStage, Enum plantType, bool isWatered)
    {
        this._currrentGrowthStage = plantGrowthStage;
        this._plantType = plantType;
        this.isPlantWatered = isWatered;
        PlantASeed();
    }
}
