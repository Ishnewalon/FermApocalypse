using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]private Sprite _plantableSoil;
    [SerializeField]private Sprite _tilledSoil;
    private GameObject _plantGO;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        _plantGO = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
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
            GetComponent<SpriteRenderer>().sprite = _tilledSoil;
            _plantGO.SetActive(true);
        }
    }

}





