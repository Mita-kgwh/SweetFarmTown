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
            questDatas[i].isComplete = true;
        }
    }

    public void VisualizeQuest()
    {
        string curScene = SceneManager.GetActiveScene().name;
        QuestData questData = questDatas.Find(x => x.sceneName == curScene);
        if (questData == null) { return; }
        //Debug.Log("Found quest");
        questData.Visualize();
    }

    internal void CompleteQuest()
    {
        string curScene = SceneManager.GetActiveScene().name;
        QuestData questData = questDatas.Find(x => x.sceneName == curScene);
        if (questData == null) { return; }
        questData.CompleteQuest();
    }
}
