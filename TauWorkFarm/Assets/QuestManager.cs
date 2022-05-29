using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestContainer questContainer;

    private static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<QuestManager>();
            return instance;
        }
    }

    public void VisualizeQuest()
    {
        Debug.Log("Visualize quest");
        questContainer.VisualizeQuest();
    }

    public void CompleteQuest()
    {
        questContainer.CompleteQuest();
    }

    public void ResetQuestAll()
    {
        questContainer.ResetAll();
    }
}
