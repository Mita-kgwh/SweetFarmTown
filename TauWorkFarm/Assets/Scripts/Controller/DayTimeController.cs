using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f; // 15 minutes chunk of time
    const float phaseInDay = secondsInDay / phaseLength; // secondsInDay divided phaseLength

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] float startAtTime = 21600f;
    [SerializeField] float morningTime = 28800f;    

    private float time;
    private int days = 0;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float timeScale = 60f;
    [SerializeField] Light2D globalLight;

    List<TimeAgent> agents;

    private PlayerData playerData;

    private bool isInside;
    private bool isIndark;

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
        playerData = DataManager.Instance.GetPlayerData();
        time = playerData.curTime;
    }

    private void OnDestroy()
    {
        playerData.curTime = time;
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            SkipTime(hours: 4);
            Debug.Log("Skip");
        }
    }

    int oldPhase = -1;
    private void TimeAgentsDo()
    {
        if (oldPhase == -1)
        {
            oldPhase = CaculatePhase();
        }

        int currentPhase = CaculatePhase();

        while (oldPhase < currentPhase)
        {
            oldPhase += 1;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }  
        }
    }

    private int CaculatePhase()
    {
        return (int)(time / phaseLength) + (int)(days * phaseInDay); 
    }

    public void PlayerInside(bool value)
    {
        isInside = value;
    }

    public void PlayerInDark(bool value)
    {
        isIndark = value;
    }

    private void DayLight()
    {
        Color color;
        if (isInside)
        {
            color = dayLightColor;
        }
        else
        {
            if (isIndark)
            {
                color = nightLightColor;
            }
            else
            {
                float value = GetTimeCurve();
                color = Color.Lerp(dayLightColor, nightLightColor, value);
            }
        }
        globalLight.color = color;
    }

    public float GetTimeCurve()
    {
        return nightTimeCurve.Evaluate(Hours);
    }

    private void TimeValueCaculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        timeText.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void NextDay()
    {
        time -= secondsInDay;
        days += 1;
    }

    public void SkipTime(float secs = 0, float mins = 0, float hours = 0)
    {
        float timetoskip = secs;
        timetoskip += 60 * mins;
        timetoskip += 3600 * hours;
        Debug.Log("Add time " + timetoskip);
        time += timetoskip;
    }

    public void SkipToMorning()
    {
        float secondsToSkip = 0f;
        if (time > morningTime)
        {
            secondsToSkip += secondsInDay - time + morningTime;
        }
        else
        {
            secondsToSkip += morningTime - time;
        }
        SkipTime(secondsToSkip);
    }
}
