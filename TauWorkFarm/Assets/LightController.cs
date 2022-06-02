using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] DayTimeController timeController;
    [SerializeField] Color nightLightColor;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] List<Light2D> lights;

    void Start()
    {
        timeController = GamesManager.Instance.dayTimeController;        
    }

    void Update()
    {
        DayLight();
    }
    private void DayLight()
    {
        float value = timeController.GetTimeCurve();
        Color color = Color.Lerp(dayLightColor, nightLightColor, value);
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].color = color;
            lights[i].intensity = value;
        }
    }

    internal void SetIntensity(int v)
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = v;
        }
    }
}
