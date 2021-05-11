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

    private Transform playerPos;

    private Transform itemSlotContainer;
    
    private Transform itemSlotTemplate;

    private Transform _canvas;

    private Item equipedItem;

    public GameObject go;

    Dictionary<Transform, Item> _inventorySlots = new Dictionary<Transform, Item>();
    
    float itemSlotOffset = 98f;

    private float x_start = 1150;
    private float y_start = -280;

    //[SerializeField] private GameObject _itemSlotContainer;

    private void Awake()
    {
    }
    
    public void SetInventory(Inventory inventory, Transform player)
    {
        _inventory = inventory;

        playerPos = player;
        
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
        ClearInventory();
        
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
            AddEvent(itemSlotObj, EventTriggerType.PointerEnter, delegate {OnPointerEnter(item, itemSlotObj);});
            AddEvent(itemSlotObj, EventTriggerType.PointerExit, delegate {OnPointerExit(itemSlotObj);});

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

    private void ClearInventory()
    {
        foreach (Transform child in itemSlotContainer.transform)
        {
            if (child != itemSlotTemplate && child.tag.Equals("slot"))
            {
                Destroy(child.gameObject);
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _inventory.DropItem(item, playerPos.position);
        }else if (item.IsEquipable() && item != equipedItem)
        {
            GameManager.Instance._currentHeldItem = item;
            equipedItem = item;
        }else if (item == equipedItem)
        {
            GameManager.Instance._currentHeldItem = new Item { itemType = Item.ItemType.EmptyHand, itemClass = Item.ItemClass.Tools, amount = 1 };
            equipedItem = null;
        }
        RefreshInventory();
    }

    private void OnPointerEnter(Item item, Transform itemSlotObj)
    {
        var text = itemSlotObj.Find("Item_Name");
        text.GetComponent<TextMeshProUGUI>().SetText(item.GetDesc());
        text.gameObject.SetActive(true);
    }

    private void OnPointerExit(Transform itemSlotObj)
    {
        itemSlotObj.Find("Item_Name").gameObject.SetActive(false);
    }
}
