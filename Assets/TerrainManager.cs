using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]private Sprite _plantableSoil;
    [SerializeField]private Sprite _tilledSoil;
    [SerializeField]private Sprite _plant0;
    Dictionary<String, List<Sprite>> _plantSprites = new Dictionary<string, List<Sprite>>();
    List<Sprite> _eggplants = new List<Sprite>();

private GameObject _plantGO;
// Start is called before the first frame update
void Start()
{
_plantGO = transform.Find("Plant").gameObject;
for (int i = 0; i < 4; i++)
{
    _eggplants.Add((Sprite)Resources.Load("Sprites/Plants/bean" + i));
}
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
            _plantGO.GetComponent<SpriteRenderer>().sprite = _plant0;
        }
        else
        {
            /*GetComponent<SpriteRenderer>().sprite = _afterHoe;*/

        }
    }

}





