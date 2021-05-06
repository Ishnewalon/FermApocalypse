using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]private Sprite _plantableSoil;
    [SerializeField]private Sprite _tilledSoil;
    private GameObject _plantGO;

    void Start()
    {
        _plantGO = gameObject.transform.GetChild(0).gameObject;
    }

    private void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().sprite == _plantableSoil && GameManager.Instance._currentHeldItem.itemType == Item.ItemType.Hoe)
        {
            TillTheSoil();
        }

        if (GetComponent<SpriteRenderer>().sprite == _tilledSoil &&
            GameManager.Instance._currentHeldItem.itemClass == Item.ItemClass.Seeds)
        {
            PlantASeed();
            _plantGO.GetComponent<PlantGrowingController>()
                .setStateData(0, GameManager.Instance._currentHeldItem.itemType, false);
        }
    }

    private void PlantASeed()
    {
        _plantGO = gameObject.transform.GetChild(0).gameObject;
        _plantGO.SetActive(true);
    }
    
    public PlantStateData getStateData()
    {
        if (_plantGO.activeSelf)
        {
            return _plantGO.GetComponent<PlantGrowingController>().getStateData();
        }
        return new PlantStateData();
    }

    public void setStateData(PlantStateData plantStateData)
    {
        if (plantStateData.isPlanted)
        {
            TillTheSoil();
            PlantASeed();
            if (plantStateData.isWatered)
            {
                TerrainWatered();
            }
            else
            {
                TerrainDry();
            }
            _plantGO.GetComponent<PlantGrowingController>().setStateData(plantStateData.plantGrowthStage, 
                                                                        plantStateData.plantType,
                                                                        plantStateData.isWatered);
        }
    }

    public void TerrainWatered()
    {
        GetComponent<SpriteRenderer>().color = new Color(111f, 104f, 104f);
    }

    public void TerrainDry()
    {
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);
    }

    public void TillTheSoil()
    {
        GetComponent<SpriteRenderer>().sprite = _tilledSoil;
    }
    public void PlantHasBeenHarvested()
    {
        GetComponent<SpriteRenderer>().sprite = _plantableSoil;
    }
}





