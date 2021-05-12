using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.itemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        
        return itemWorld;
    }

    private Item _item;
    private SpriteRenderer _spriteRenderer;
    private Light2D _light2D;
    private TextMeshPro _textMeshPro;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light2D = transform.Find("ItemGlow").GetComponent<Light2D>();
        _textMeshPro = transform.Find("Amount").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = item.GetSprite();
        _light2D.color = item.GetColor();
        if (item.amount > 1)
        {
            _textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            _textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return _item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
