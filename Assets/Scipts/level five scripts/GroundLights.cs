using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GroundLights : MonoBehaviour
{
    
    [SerializeField] public Light2D[] groundLights;

    public float repeatRate { get; set; } = 2f;
    public Color lightColor { get; set; } = new Color(1,0,0);

    private int swipSwapVariable = 1;

    public void Start()
    {
        StartCoroutine(flashLights());
    }

    public IEnumerator flashLights()
    {
        InvokeRepeating("updateLights", 1, repeatRate);
        yield return null;
    }

    public void updateLights()
    {
        foreach(Light2D light in groundLights)
        {
            if (swipSwapVariable == 1)
                light.enabled = false;
            else
                light.enabled = true;
        }
        swipSwapVariable *= -1;
    }

    public void updateLightColor(Color color)
    {
        lightColor = color;
        foreach (Light2D light in groundLights)
        {
            light.color = lightColor;
        }
    }
}
