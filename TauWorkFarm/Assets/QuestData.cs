using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest Data")]
public class QuestData : ScriptableObject
{
    public bool isComplete;
    public string sceneName;
    public string questName;
    //public bool canDo;
    public List<GameObject> gameObjects;
    public List<RewardActions> rewards;

    public void CompleteQuest()
    {
        isComplete = true;
        for (int i = 0; i < rewards.Count; i++)
        {
            rewards[i].UnlockedNewItemApply();
            rewards[i].GivenItem();
            rewards[i].MoneyReward();
        }
    }

    internal void Visualize()
    {
        if (isComplete)// || canDo == false)
        {
            return;
        }
        Debug.Log("visualize obj");

        for (int i = 0; i < gameObjects.Count; i++)
        {
            Vector3 position = gameObjects[i].transform.position;
            GameObject spawnObject = Instantiate(gameObjects[i], position, Quaternion.identity);
        }

    }
}
