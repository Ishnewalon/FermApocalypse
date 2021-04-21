using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.08f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2 (x: Random.Range(-1, 2)*speed, y: Random.Range(-1, 2) * speed));
    }
}
