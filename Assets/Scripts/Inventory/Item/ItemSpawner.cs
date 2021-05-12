using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public Item _item;

    private void Awake()
    {
        ItemWorld.SpawnItemWorld(transform.position, _item);
        Destroy(gameObject);
    }
}
