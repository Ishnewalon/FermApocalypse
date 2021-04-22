using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{

    private Inventory _inventory;

    public void SetInventory(Inventory inventory)
    {
        this._inventory = inventory;
    }
}
