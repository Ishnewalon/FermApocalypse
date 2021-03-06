using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform itemWorld;

    public Sprite hoeSprite;
    public Sprite scytheSprite;
    public Sprite waterBucketSprite;
    public Sprite GoldenBucketSprite;
    public Sprite GoldenHoeSprite;
    public Sprite GoldenScytheSprite;
    public Sprite LimeScytheSprite;
    public Sprite CandyHoeSprite;
    public Sprite FireBucketSprite;
    
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

    public Sprite beanSeeds;
    public Sprite blueberrySeeds;
    public Sprite cabbageSeeds;
    public Sprite carrotSeeds;
    public Sprite cauliflowerSeeds;
    public Sprite celerySeeds;
    public Sprite cornSeeds;
    public Sprite eggplantSeeds;
    public Sprite grapeSeeds;
    public Sprite leakSeeds;
    public Sprite onionSeeds;
    public Sprite pepperSeeds;
    public Sprite pineappleSeeds;
    public Sprite pumpkinSeeds;
    public Sprite rockmelonSeeds;
    public Sprite squashSeeds;
    public Sprite strawberrySeeds;
    public Sprite tomatoSeeds;
    public Sprite turnipSeeds;
    public Sprite watermelonSeeds;

    public Texture2D basicHoe;
    public Texture2D basicScythe;
    public Texture2D basicBucket;
    public Texture2D goldenBucket;
    public Texture2D goldenHoe;
    public Texture2D goldenScythe;
    public Texture2D limeScythe;
    public Texture2D candyHoe;
    public Texture2D fireBucket;
}
