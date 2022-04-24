using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    float time;
    private int days = 0;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float timeScale = 60f;
    [SerializeField] Light2D globalLight;

    float Hours
    {
        get
        {
            return time / 3600f;
        }
    }

    float Minutes
    {
        get
        {
            return time % 3600f / 60f;
        }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int hh = (int)Hours;
        int mm = (int)Minutes;
        timeText.text = hh.ToString("00") + ":" + mm.ToString("00");
        float value = nightTimeCurve.Evaluate(Hours);
        Color color = Color.Lerp(dayLightColor, nightLightColor, value);
        globalLight.color = color;
        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
