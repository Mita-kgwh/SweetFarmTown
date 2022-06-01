using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestContainer questContainer;
    [SerializeField] GameObject rewardPanelObj;

    private RewardsPanel rewardsPanel;

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

    private void Awake()
    {
        rewardsPanel= rewardPanelObj.GetComponent<RewardsPanel>();
    }

    private void Start()
    {
        VisualizeQuest();
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

    public void ShowRewards()
    {
        rewardPanelObj.SetActive(true);
    }

    public RewardsPanel GetRewardsPanel()
    {
        return rewardsPanel;
    }
}
