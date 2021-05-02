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

    Dictionary<Transform, Item> _inventorySlots = new Dictionary<Transform, Item>();
    
    float itemSlotCellSize = 60f;
    float itemSlotOffset = 98f;

    private int x_start = 1000;
    private int y_start = -280;

    //[SerializeField] private GameObject _itemSlotContainer;

    private void Awake()
    {
    }

    private void Update()
    {
        refreshInventory();
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        
        go = Instantiate(gameObject);

        itemSlotContainer = go.transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        inventory.OnItemListChanged += InventoryOnItemListChanged;
        
        CreateSlots();
    }

    private void InventoryOnItemListChanged(object sender, EventArgs e)
    {
        CreateSlots();
    }

    private void CreateSlots()
    {
        var index = 0;
        var invList = _inventory.GetItemList();
        _inventorySlots = new Dictionary<Transform, Item>();
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var itemSlotObj = Instantiate(itemSlotTemplate, itemSlotContainer);
                itemSlotObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x_start + j * itemSlotOffset, y_start + i * -itemSlotOffset);
                
                AddEvent(itemSlotObj, EventTriggerType.EndDrag, delegate {OnDragEnd(itemSlotObj);});

                if (index < invList.Count)
                {
                    _inventorySlots.Add(itemSlotObj, invList[index]);
                    print("Item added!");
                }
                else
                {
                    _inventorySlots.Add(itemSlotObj, null);
                }
                index++;
            }
        }
    }

    private void refreshInventory()
    {
        print(_inventorySlots.Count);
        foreach (KeyValuePair<Transform, Item> _slot in _inventorySlots)
        {
            print("hello????");
            if (_slot.Value != null)
            {
                print("not null");
                _slot.Key.Find("Item").GetComponent<Image>().sprite = _slot.Value.GetSprite();
                _slot.Key.Find("Item_Qty").GetComponent<TextMeshProUGUI>().SetText(
                    _slot.Value.amount > 1 ? _slot.Value.amount.ToString() : "");
                
                _slot.Key.gameObject.SetActive(true);
                
                AddEvent(_slot.Key, EventTriggerType.PointerEnter, delegate {OnPointerEnter(_slot.Key);});
                AddEvent(_slot.Key, EventTriggerType.PointerExit, delegate {OnPointerExit(_slot.Key);});
                AddEvent(_slot.Key, EventTriggerType.BeginDrag, delegate {OnDragStart(_slot.Key);});
                AddEvent(_slot.Key, EventTriggerType.Drag, delegate {OnDrag(_slot.Key);});
                AddEvent(_slot.Key, EventTriggerType.PointerClick, delegate {OnPointerClick(_slot.Key);});
            }
            else
            {
                _slot.Key.gameObject.SetActive(false);
            }
        }
    }
    
    private void AddEvent(Transform slotTransform, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = slotTransform.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry eventTrigger = new EventTrigger.Entry();
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
            uiText.SetText(item.amount > 1 ? item.amount.ToString() : "");

            x++;
            if (x > 1)
            {
                x = -2;
                y--;
            }
        }
    }
}
