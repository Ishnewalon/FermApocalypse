using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{

    private Inventory _inventory;

    private Transform itemSlotContainer;
    
    private Transform itemSlotTemplate;

    private Transform _canvas;

    private Item equipedItem;

    public GameObject go;

    Dictionary<Transform, Item> _inventorySlots = new Dictionary<Transform, Item>();
    
    float itemSlotCellSize = 60f;
    float itemSlotOffset = 98f;

    private int x_start = 1000;
    private int y_start = -280;

    //[SerializeField] private GameObject _itemSlotContainer;

    private void Awake()
    {
    }
    
    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        
        go = Instantiate(gameObject);

        itemSlotContainer = go.transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        inventory.OnItemListChanged += InventoryOnItemListChanged;
        
        RefreshInventory();
    }

    private void InventoryOnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        var column = 0;
        var row = 0;
        foreach (var item in _inventory.GetItemList())
        {
            var itemSlotObj = Instantiate(itemSlotTemplate, itemSlotContainer);
            itemSlotObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x_start + column * itemSlotOffset, y_start + row * -itemSlotOffset);

            if (item == equipedItem)
            {
                itemSlotObj.Find("Slot_Background").GetComponent<Image>().color = Color.red;
            }

            AddEvent(itemSlotObj, EventTriggerType.PointerClick, delegate {OnPointerClick(item);});

            itemSlotObj.Find("Item").GetComponent<Image>().sprite = item.GetSprite();
            itemSlotObj.Find("Item_Qty").GetComponent<TextMeshProUGUI>().SetText(
                item.amount > 1 ? item.amount.ToString() : "");
            
            itemSlotObj.gameObject.SetActive(true);
            
            column++;
            if (column > 5)
            {
                column = 0;
                row++;
            }
        }
    }

    private void AddEvent(Transform slotTransform, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = slotTransform.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener((data) => { action((PointerEventData)data);});
        trigger.triggers.Add(eventTrigger);
    }

    private void OnPointerClick(Item item)
    {
        if (item.IsEquipable())
        {
            GameManager.Instance._currentHeldItem = item.itemType;
            equipedItem = item;
        }
        RefreshInventory();
    }

    private void OnPointerEnter(Transform slotTransform)
    {
        throw new NotImplementedException();
    }

    private void OnPointerExit(Transform slotTransform)
    {
        
    }

    private void OnDragStart(Transform slotTransform)
    {
        
    }
    
    private void OnDragEnd(Transform slotTransform)
    {
        
    }
    
    private void OnDrag(Transform slotTransform)
    {
        
    }
}
