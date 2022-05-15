using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    DisableControls disableControls;
    DayTimeController timeController;

    private void Awake()
    {
        disableControls = GetComponent<DisableControls>();
        timeController = GamesManager.Instance.dayTimeController;
    }
    internal void DoSleep()
    {
        StartCoroutine(SleepRoutine());
    }

    IEnumerator SleepRoutine()
    {
        ScreenTint screenTint = GamesManager.Instance.screenTint;
        disableControls.DisableControl();

        screenTint.Tint();
        yield return new WaitForSeconds(2f);

        PlayerManager.Instance.FullHeal();
        PlayerManager.Instance.FullRest();
        timeController.SkipToMorning();

        screenTint.UnTint();
        yield return new WaitForSeconds(1.5f);

        disableControls.EnableControl();

        yield return null;
    }
}
