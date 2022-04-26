using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f; // 15 minutes chunk of time

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] float startAtTime = 21600f;

    float time;
    private int days = 0;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float timeScale = 60f;
    [SerializeField] Light2D globalLight;

    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void UnSubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

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

    private void Start()
    {
        time = startAtTime;
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        TimeValueCaculation();
        DayLight();

        if (time > secondsInDay)
        {
            NextDay();
        }

        TimeAgentsDo();
    }

    int oldPhase = 0;
    private void TimeAgentsDo()
    {
        int currentPhase = (int)(time / phaseLength);
        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }

    }

    private void DayLight()
    {
        float value = nightTimeCurve.Evaluate(Hours);
        Color color = Color.Lerp(dayLightColor, nightLightColor, value);
        globalLight.color = color;
    }

    private void TimeValueCaculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        timeText.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
