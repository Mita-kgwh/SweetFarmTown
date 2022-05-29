using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardActions : ScriptableObject
{
    public virtual void UnlockedNewItemApply()
    {
        Debug.Log("No unblockednewitem action implement");
        return;
    }

    public virtual void GivenItem()
    {
        Debug.Log("No givenitem action implement");
        return;
    }

    public virtual void MoneyReward()
    {
        Debug.Log("No moneyreward action implement");
        return;
    }
}
