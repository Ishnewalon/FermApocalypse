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

    void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite == _tilledSoil && _plantGO.activeSelf == false)
        {
            GetComponent<SpriteRenderer>().sprite = _plantableSoil;
        }
    }

    private void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().sprite == _plantableSoil)
        {
            PlantASeed();
        }
    }

    private void PlantASeed()
    {
        GetComponent<SpriteRenderer>().sprite = _tilledSoil;
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
            PlantASeed();
            _plantGO.GetComponent<PlantGrowingController>().setStateData(plantStateData.plantGrowthStage, plantStateData.plantType);
        }
    }
}





