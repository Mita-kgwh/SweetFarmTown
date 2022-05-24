using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInteract : Interactable
{
    public MissionTargetID id;

    [SerializeField] MissionTargetObject mission;

    public override void Interact(PlayerController player)
    {
        mission = MissionManager.Instance.SpawnAMission(id);
        if (mission == null)
        {
            Debug.Log("Mission Null");
        }
    }
}
