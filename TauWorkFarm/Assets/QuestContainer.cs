using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Data/Container/Quest Container")]
public class QuestContainer : ScriptableObject
{
    public List<QuestData> questDatas;

    public void ResetAll()
    {
        for (int i = 0; i < questDatas.Count; i++)
        {
            questDatas[i].isComplete = false;
        }
    }

    public void VisualizeQuest()
    {
        string curScene = SceneManager.GetActiveScene().name;
        List<QuestData> questDataList = questDatas.FindAll(x => x.sceneName == curScene);
        if (questDataList == null) { return; }
        //Debug.Log("Found quest");
        for (int i = 0; i < questDataList.Count; i++)
        {
            questDataList[i].Visualize();
        }
        
    }

    internal void CompleteQuest(string _questName)
    {
        //string curScene = SceneManager.GetActiveScene().name;
        QuestData questData = questDatas.Find(x => x.questName == _questName);
        if (questData == null) { return; }
        questData.CompleteQuest();
    }
}
