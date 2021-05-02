using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowingController : MonoBehaviour
{
    private int _currrentGrowthStage = 0;
    private Enum _plantType;
    private const int _DaysNeededForMaxGrowth = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.GOTOBED)
        {
            _currrentGrowthStage++;
            if ((_currrentGrowthStage < 4) && (_currrentGrowthStage >= 0))
            {
                MakePlantGrow();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true && _currrentGrowthStage == 0 && GameManager.Instance._cropSeedEnum.Contains(GameManager.Instance._currentHeldItem))
        {
            _plantType = GameManager.Instance._currentHeldItem;
            PlantASeed();
        }
    }

    /*private void OnMouseDown()
    {
        if (!GameManager.Instance._cropSeedEnum.Contains(GameManager.Instance._currentHeldItem))
        {
            
        }
    }*/

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
        return new PlantStateData(_currrentGrowthStage, _plantType);
    }

    public void setStateData(int plantGrowthStage, Enum plantType)
    {
        this._currrentGrowthStage = plantGrowthStage;
        this._plantType = plantType;
        PlantASeed();
    }
}
