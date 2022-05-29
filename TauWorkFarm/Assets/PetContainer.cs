using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName ="Data/Container/Pet Container")]
public class PetContainer : ScriptableObject
{
    public List<PetData> petDatas;
    public List<GameObject> petPrefabs;

    public void VisualizePet()
    {
        string curScene = SceneManager.GetActiveScene().name;
        List<PetData> datas = petDatas.FindAll(x => x.sceneName == curScene);
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject spawnPet = Instantiate(petPrefabs[datas[i].kind], datas[i].position, Quaternion.identity);
            spawnPet.GetComponent<PetManager>().SetData(datas[i]);
        }
    }

    internal void AddPet(PetData petData)
    {
        petDatas.Add(petData);
    }

    internal void Clear()
    {
        petDatas = new List<PetData>();
    }
}
