using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionData
{
    public int id = -1;
    public Item item;
    public int count;
    public string description;
    public bool isComplete;

    public MissionData()
    {
        id = -1;
        item = null;
        count = 0;
        description = "";
        isComplete = false;
    }

    internal void Clear()
    {
        item = null;
        count = 0;
        description = "";
        isComplete = false;
    }

    //public MissionData(Item _item, int _count, string _description, bool _isComplete)
    //{
    //    item = _item;
    //    count = _count;
    //    description = _description;
    //    isComplete = _isComplete;
    //}

}

[RequireComponent(typeof(TimeAgent))]
public class MissionPanel : MonoBehaviour
{
    public MissionContainer missionContainer;

    public List<DataSlot> dataSlots;

    [SerializeField] TimeAgent timeAgent;
    [SerializeField] int missionPerTime; // 1 chunk tine is 15 minutes
    int countTime = 0;

    //keu mission manager spawn ra mission con trong r add vao mission container de luu luon, ok

    private void Start()
    {
        timeAgent.onTimeTick += SpawnNewMission;
        if (missionContainer.missionDatas.Count == 0) { missionContainer.Init(); }
        SetIndex();
        Show();
    }

    private void SpawnNewMission()
    {
        if (!missionContainer.CheckFreeSpace())
        {
            Debug.Log("full mission");
            return;
        }
        Debug.Log("Spawn count" + countTime);
        countTime++;
        if (countTime == missionPerTime)
        {
            countTime = 0;
            MissionTargetObject mission = MissionManager.Instance.SpawnAMission(MissionTargetID.COLLECTING_RESOURCE);
            if (mission == null)
            {
                Debug.Log("mission null");
                return;
            }
            missionContainer.AddData(mission.Data);

        }
    }

    private void OnEnable()
    {
        Clear();
        Show();
    }

    private void LateUpdate()
    {
        if (missionContainer == null) { return; }
        if (missionContainer.isChanging)
        {
            Show();
            missionContainer.isChanging = false;
        }
    }

    public void SetIndex()
    {
        for (int i = 0; i < dataSlots.Count; i++)
        {
            dataSlots[i].SetMissionPanel(this);
            dataSlots[i].SetIndex(i);
        }
    }
    public virtual void Show()
    {
        if (missionContainer == null) { return; }

        for (int i = 0; i < missionContainer.missionDatas.Count && i < dataSlots.Count; i++)
        {
            if (missionContainer.missionDatas[i].item == null)
            {
                dataSlots[i].SetEmpty();
            }
            else
            {
                dataSlots[i].Set(missionContainer.missionDatas[i]);
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < dataSlots.Count; i++)
        {
            dataSlots[i].SetEmpty();
        }
    }
}
