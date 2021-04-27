using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    private SpriteRenderer _renderer;

    [SerializeField]private Sprite _beforeHoe;
    [SerializeField]private Sprite _afterHoe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        _renderer = (SpriteRenderer)gameObject.GetComponent<SpriteRenderer>();
        Debug.Log("Terrain has been clicked");
        if (_renderer.sprite == _afterHoe)
        {
            _renderer.sprite = _beforeHoe;
        }
        else
        {
            _renderer.sprite = _afterHoe;
        }
    }

}
