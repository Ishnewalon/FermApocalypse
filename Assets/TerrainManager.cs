using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]private Sprite _plantableSoil;
    [SerializeField]private Sprite _tilledSoil;
    
    private Sprite[] _allPlantSprites;

     

    private GameObject _plantGO;
    // Start is called before the first frame update
    void Start()
    {
        int j = 0;
        _allPlantSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plants");
        for (int i = 0; i < _allPlantSprites.Length; i++)
        {
            print(_allPlantSprites[i].name);
        }
        
        
        /*for (int i = 28; i < 32; i++)
        {
            _eggplants.Add(_allPlantSprites[i]);
            print(_eggplants[j]);
            j++;
        }
        
        _plantGO = transform.Find("Plant").gameObject;
        
        
        _plantSprites.Add(Item.ItemType.EggPlantSeed, _eggplants);*/
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().sprite == _tilledSoil && _plantGO.activeSelf == false)
        {
            GetComponent<SpriteRenderer>().sprite = _plantableSoil;
        }
    }

    /*private void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().sprite == _plantableSoil)
        {
            GetComponent<SpriteRenderer>().sprite = _tilledSoil;
            _plantGO.SetActive(true);
            _plantGO.GetComponent<SpriteRenderer>().sprite = _plantSprites[Item.ItemType.EggPlantSeed][_currrentPlantSprite];
        }
        else if ((GetComponent<SpriteRenderer>().sprite == _tilledSoil) && (_currrentPlantSprite > 4) && (_currrentPlantSprite != 0))
        {
            _currrentPlantSprite++;
            _plantGO.GetComponent<SpriteRenderer>().sprite = _plantSprites[Item.ItemType.EggPlantSeed][_currrentPlantSprite];
        }
        else if ((GetComponent<SpriteRenderer>().sprite == _tilledSoil) && (_currrentPlantSprite == 3))
        {
            _plantGO.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = _plantableSoil;
        }
    }*/

}





