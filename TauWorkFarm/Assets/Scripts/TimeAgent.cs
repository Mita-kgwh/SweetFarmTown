using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick; 

    // Start is called before the first frame update
    private void Start()
    {
        GamesManager.Instance.dayTimeController.Subscribe(this);
    }

    //invoke by daytimecontroller to tell timeagent that time have pass
    public void Invoke()
    {
        onTimeTick?.Invoke();
    }

    private void OnDestroy()
    {
        if (GamesManager.Instance == null)
        {
            Debug.Log("GameManager have destroyed before the tree");
            return;
        }
        GamesManager.Instance.dayTimeController.UnSubscribe(this);
    }

}
