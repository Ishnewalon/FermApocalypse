using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{

    private Inventory _inventory;

    private Transform itemSlotContainer;
    
    private Transform itemSlotTemplate;

    private Transform _canvas;

    public GameObject go;

    //[SerializeField] private GameObject _itemSlotContainer;

    private void Awake()
    {
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        
        go = Instantiate(gameObject);
        _canvas = go.transform.GetChild(0);
        
        itemSlotContainer = go.transform.GetChild(0).Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        inventory.OnItemListChanged += InventoryOnItemListChanged;
        
        RefreshInventoryItems();
    }

    private void InventoryOnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        
        int x = -2;
        int y = 1;
        float itemSlotCellSize = 60f;
        float itemSlotOffset = 30f;
        foreach (var item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotContainer, _canvas).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + itemSlotOffset, y * itemSlotCellSize);
            
            Image image = itemSlotRectTransform.Find("itemSlotTemplate").Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            
            x++;
            if (x > 1)
            {
                x = -2;
                y--;
            }
        }
    }
}
