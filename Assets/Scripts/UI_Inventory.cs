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

    public GameObject go;

    private Dictionary<Transform, Item> _inventorySlots = new Dictionary<Transform, Item>();
    
    float itemSlotCellSize = 60f;
    float itemSlotOffset = 30f;

    //[SerializeField] private GameObject _itemSlotContainer;

    private void Awake()
    {
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        
        go = Instantiate(gameObject);
        CreateSlots();

        itemSlotContainer = go.transform.GetChild(0).Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        inventory.OnItemListChanged += InventoryOnItemListChanged;
        
        //RefreshInventoryItems();
    }

    private void InventoryOnItemListChanged(object sender, EventArgs e)
    {
        //RefreshInventoryItems();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var itemSlotObj = Instantiate(itemSlotTemplate, itemSlotContainer);
                itemSlotObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(j * itemSlotOffset, i * itemSlotOffset);
                
                AddEvent(itemSlotObj, EventTriggerType.PointerEnter, delegate {OnPointerEnter(itemSlotObj);});
                AddEvent(itemSlotObj, EventTriggerType.PointerExit, delegate {OnPointerExit(itemSlotObj);});
                AddEvent(itemSlotObj, EventTriggerType.BeginDrag, delegate {OnDragStart(itemSlotObj);});
                AddEvent(itemSlotObj, EventTriggerType.EndDrag, delegate {OnDragEnd(itemSlotObj);});
                AddEvent(itemSlotObj, EventTriggerType.Drag, delegate {OnDrag(itemSlotObj);});
                AddEvent(itemSlotObj, EventTriggerType.PointerClick, delegate {OnPointerClick(itemSlotObj);});
                
                _inventorySlots.Add(itemSlotObj, null);
            }
        }
    }
    
    private void AddEvent(Transform slotTransform, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = slotTransform.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    private void OnPointerClick(Transform slotTransform)
    {
        
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

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        
        int x = -2;
        int y = 1;
        foreach (var item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotContainer, _canvas).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + itemSlotOffset, y * itemSlotCellSize);
            
            Image image = itemSlotRectTransform.Find("itemSlotTemplate").Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("itemSlotTemplate").Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x++;
            if (x > 1)
            {
                x = -2;
                y--;
            }
        }
    }
}
