using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public Vector3 playerPosition;
    public int money;
    public int curStamina;
    public string curSceneName;

    public PlayerData()
    {
        ReSet();
    }

    public void ReSet()
    {
        playerPosition = new Vector3();
        money = 1000;
        curStamina = 100;
        curSceneName = "StartScene";
    }
}
