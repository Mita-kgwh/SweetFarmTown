using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingResource : MissionTargetObject
{
    public override MissionTargetID ID { get => MissionTargetID.COLLECTING_RESOURCE; }

    public override MissionData Data { get => MissionData; }

    public MissionData MissionData 
    {
        get
        {
            MissionData missionData = new MissionData();
            missionData.item = GamesManager.Instance.itemDB.items[5];
            missionData.count = 5;
            missionData.description = "Hay thu thap du cac vat pham nay";
            missionData.isComplete = false;
            return missionData;
        }
    }
}
