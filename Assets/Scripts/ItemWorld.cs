using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{

    public static ItemWorld SpawnItemWorld(Item item)
    {
        return null;
    }

    private Item _item;

    public void SetItem(Item item)
    {
        this._item = item;
    }

}
