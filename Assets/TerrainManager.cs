using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]private Sprite _plantableSoil;
    [SerializeField]private Sprite _tilledSoil;
    Dictionary<Enum, List<Sprite>> _plantSprites = new Dictionary<Enum, List<Sprite>>();
    List<Sprite> _eggplants = new List<Sprite>();
    private int _currrentPlantSprite = 0;
    private Sprite[] _allPlantSprites; 

    private GameObject _plantGO;
    // Start is called before the first frame update
    void Start()
    {
        int j = 0;
        _allPlantSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plants");
        
        print(_allPlantSprites.Length);

        for (int i = 28; i < 32; i++)
        {
            _eggplants.Add(_allPlantSprites[i]);
            print(_eggplants[j]);
            j++;
        }
        
        _plantGO = transform.Find("Plant").gameObject;
        
        
        _plantSprites.Add(Item.ItemType.EggPlantSeed, _eggplants);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            ChangeSprite();
        }*/
    }

    private void OnMouseDown()
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
    }

}





