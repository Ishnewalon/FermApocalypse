using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.itemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        //itemWorld.SetItem(item);
        
        return itemWorld;
    }

    private Item _item;
    private SpriteRenderer _spriteRenderer;
    private Light2D _light2D;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light2D = transform.Find("ItemGlow").GetComponent<Light2D>();
    }

    public void SetItem(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = item.GetSprite();
        _light2D.color = item.GetColor();
    }

}
