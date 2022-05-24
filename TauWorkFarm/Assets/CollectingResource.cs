using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingResource : MissionTargetObject
{
    public override MissionTargetID ID { get => MissionTargetID.COLLECTING_RESOURCE; }

    public override void Accept()
    {
        base.Accept();
        Debug.Log("accept collecting");
    }

    public override void Decline()
    {
        PlayerManager.Instance.DecreaseCurrency(100);
    }

    public override void Complete()
    {
        PlayerManager.Instance.AddCurrency(100);
    }
}
