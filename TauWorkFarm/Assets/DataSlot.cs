using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DataSlot : MonoBehaviour
{
    //Item item;
    Slot slot = new Slot();
    int id;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] GameObject buttonDone;
    [SerializeField] GameObject buttonSkip;
    [SerializeField] float gainPercent = 1.2f;
    [SerializeField] float lossPercent = 0.7f;


    MissionPanel missionPanel;

    public void SetMissionPanel(MissionPanel source)
    {
        missionPanel = source;
    }
    public void SetIndex(int i)
    {
        id = i;
    }

    public int GetIndex()
    {
        return id;
    }
    public void SetEmpty()
    {
        icon.sprite = null;
        description.text = "";
        buttonDone.SetActive(false);
        buttonSkip.SetActive(false);
    }

    internal void Set(MissionData missionData)
    {
        description.text = missionData.description;
        slot.item = missionData.item;
        slot.count = missionData.count;
        icon.sprite = slot.item.icon;
        buttonDone.SetActive(true);
        buttonSkip.SetActive(true);
    }

    public void CompleteMission()
    {
        ItemContainer itemContainer = GamesManager.Instance.inventoryContainer;
        if (itemContainer.CheckItem(slot))
        {
            int moneygain = (int)(slot.item.price * slot.count * gainPercent);
            //MissionData.isComplete = true;
            PlayerManager.Instance.AddCurrency(moneygain);
            itemContainer.Remove(slot.item, slot.count);
            missionPanel.missionContainer.RemoveByID(id);
            return;
        }
        //Pop up message
    }

    public void SkipMission()
    {
        int moneyloss = (int)(slot.item.price * slot.count * lossPercent);
        PlayerManager.Instance.DecreaseCurrency(moneyloss);
        missionPanel.missionContainer.RemoveByID(id);
    }
}
