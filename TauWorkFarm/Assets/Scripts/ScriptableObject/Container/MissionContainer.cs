using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Container/Mission Container")]
public class MissionContainer : ScriptableObject
{
    public List<MissionData> missionDatas;
    public bool isChanging;

    public void Init()
    {
        missionDatas = new List<MissionData>();
        for (int i = 0; i < 4; i++) //4 mission max in board
        {
            MissionData mission = new MissionData();
            mission.id = i;
            missionDatas.Add(mission);
        }
    }

    public void Add(Item _item, int count, string _description, bool _isComplete)
    {
        isChanging = true;
        MissionData mission = missionDatas.Find(x => x.item == null);
        if (mission != null)
        {
            mission.item = _item;
            mission.count = count;
            mission.description = _description;
            mission.isComplete = _isComplete;
        }
    }

    internal void Clear()
    {
        for (int i = 0; i < missionDatas.Count; i++)
        {
            missionDatas[i].Clear();
        }
    }

    public void AddData(MissionData data)
    {
        isChanging = true;
        MissionData mission = missionDatas.Find(x => x.item == null);
        if (mission != null)
        {
            mission.item = data.item;
            mission.count = data.count;
            mission.description = data.description;
            mission.isComplete = data.isComplete;
        }
    }

    //public void Remove(Item _item, int count, string _description, bool _isComplete)
    //{
    //    isChanging = true;
    //    MissionData mission = missionDatas.Find(x => x.isComplete == true && x.item == _item);
    //    if (mission != null ) { mission.Clear(); }
    //}

    internal bool CheckFreeSpace()
    {
        for (int i = 0; i < missionDatas.Count; i++)
        {
            if (missionDatas[i].item == null)
            {
                return true;
            }
        }

        return false;
    }

    internal void RemoveByID(int id)
    {
        isChanging = true;
        MissionData mission = missionDatas[id];
        if (mission != null) { mission.Clear(); }
    }
}
