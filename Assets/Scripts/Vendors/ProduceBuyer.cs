using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceBuyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.UIShop.ToggleSellShop(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameManager.Instance.UIShop.ToggleSellShop(false);
    }
}
