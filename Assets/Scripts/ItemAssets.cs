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
    public Sprite waterBucketSprite;
    
    public Sprite bean;
    public Sprite blueberry;
    public Sprite cabbage;
    public Sprite carrot;
    public Sprite cauliflower;
    public Sprite celery;
    public Sprite corn;
    public Sprite eggplant;
    public Sprite grape;
    public Sprite leak;
    public Sprite onion;
    public Sprite pepper;
    public Sprite pineapple;
    public Sprite pumpkin;
    public Sprite rockmelon;
    public Sprite squash;
    public Sprite strawberry;
    public Sprite tomato;
    public Sprite turnip;
    public Sprite watermelon;
}
