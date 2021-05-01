using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowingController : MonoBehaviour
{
    private int _currrentPlantSprite = 0;
    private Enum _plantType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true && _currrentPlantSprite == 0)
        {
            _plantType = GameManager.Instance._currentHeldItem;
            GetComponent<SpriteRenderer>().sprite =
                GameManager.Instance._plantSprites[_plantType][_currrentPlantSprite];
                
        }
    }

    private void OnMouseDown()
    {
        _currrentPlantSprite++;
        print(_currrentPlantSprite);
        if ((_currrentPlantSprite < 4) && (_currrentPlantSprite >= 0))
        {
            GetComponent<SpriteRenderer>().sprite =
                GameManager.Instance._plantSprites[_plantType][_currrentPlantSprite];
        }
        else if (_currrentPlantSprite >= 4)
        {
            gameObject.SetActive(false);
            _currrentPlantSprite = 0;
        }
    }
}
