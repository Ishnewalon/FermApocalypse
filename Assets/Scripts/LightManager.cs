using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] 
    private Light2D _globalLight;
    void Update()
    {
        if ((GameManager.Instance.hours >= 17) && (GameManager.Instance.hours <= 19))
        {
            ChangeLightIntensity();
        }
    }

    public void ChangeLightIntensity()
    {
        if (_globalLight.intensity >= 0.5)
        {
            _globalLight.intensity -= 0.0001f;
        }
    }
}
