using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCampManager : TimeAgent
{
    [SerializeField] int liveOfFire;
    [SerializeField] FireInteractable fireInteractable;
    [SerializeField] int count;

    private void Start()
    {
        onTimeTick += StillFire;
        Init();
        if (fireInteractable.GetData() != null)
        {
            count = fireInteractable.GetData().count;
        }
        
    }

    private void StillFire()
    {
        if (fireInteractable.GetData() == null) { return; }
        if (fireInteractable.GetData().onFire)
        {
            count += (1 + fireInteractable.GetData().count);
            fireInteractable.GetData().count = 0;
            if (count == liveOfFire)
            {
                Destroy(gameObject);
            }
        }  
    }

    public bool IsOnFire()
    {
        return fireInteractable.GetData().onFire;
    }

    private void OnDestroy()
    {
        if (count != liveOfFire)
        {
            fireInteractable.GetData().count = count;
        }
    }
}
