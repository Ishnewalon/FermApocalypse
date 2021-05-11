using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Item _activeItem;
    private CursorMode _cursorMode = CursorMode.Auto;
    private Vector2 _hotspot = Vector2.zero;
    private Texture2D[] _cursorList;
    
    void Start()
    {
        _cursorList = Resources.LoadAll<Texture2D>("Cursors/");
    }
    
    void Update()
    {
        _activeItem = GameManager.Instance._currentHeldItem;
        if (_activeItem.itemClass == Item.ItemClass.Tools && _activeItem.itemType != Item.ItemType.EmptyHand)
        {
            switch (_activeItem.itemType)
            {
                case Item.ItemType.Hoe: UnityEngine.Cursor.SetCursor(ItemAssets.Instance.basicHoe, _hotspot, _cursorMode);
                    break;
                case Item.ItemType.WaterBucket: UnityEngine.Cursor.SetCursor(_cursorList[1], _hotspot, _cursorMode);
                    break;
                case Item.ItemType.Scythe: UnityEngine.Cursor.SetCursor(_cursorList[0], _hotspot, _cursorMode);
                    break;
            }
        }

        if (_activeItem.itemClass == Item.ItemClass.Seeds)
        {
            UnityEngine.Cursor.SetCursor(GetSeedCursor(_activeItem.itemType.ToString()), _hotspot, _cursorMode);
        }

        if (_activeItem.itemType == Item.ItemType.EmptyHand)
        {
            UnityEngine.Cursor.SetCursor(null, _hotspot, _cursorMode);
        }
    }

    private Texture2D GetSeedCursor(string seed)
    {
        foreach (var cursor in _cursorList)
        {
            print("Lookin for "+seed+" checkin "+cursor);
            if (cursor.name.Equals(seed))
            {
                return cursor;
            }
        }
        return null;
    }
}
