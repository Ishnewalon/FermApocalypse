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
            DimLightIntensity();
        }

        if ((GameManager.Instance.hours >= 7) && (GameManager.Instance.hours < 17))
        {
            brigtenLightIntensity();
        }
    }

    public void DimLightIntensity()
    {
        if (_globalLight.intensity >= 0.5)
        {
            _globalLight.intensity -= 0.0001f;
        }
    }

    public void brigtenLightIntensity()
    {
        _globalLight.intensity = 1;
    }
}
