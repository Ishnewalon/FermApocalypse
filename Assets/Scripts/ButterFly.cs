using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.08f;
    
    void Update()
    {
        transform.Translate(new Vector2 (x: Random.Range(-1, 2)*speed, y: Random.Range(-1, 2) * speed));
    }
}
