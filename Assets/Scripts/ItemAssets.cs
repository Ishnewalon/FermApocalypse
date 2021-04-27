using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    
    public static ItemAssets Instance { get; private set; }// todo : aske teach about static dont yell at me bruh!

    private void Awake()
    {
        Instance = this;
    }

    public Transform itemWorld;

    public Sprite hoeSprite;
    public Sprite scytheSprite;
    public Sprite WaterBucketSprite;
}
