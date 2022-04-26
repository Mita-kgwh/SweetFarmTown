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
        Init(); // vi no bi override o class con CropManager nen k subscribe dc
                // ta tach rieng de goi rieng o class con
    }

    public void Init()
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
