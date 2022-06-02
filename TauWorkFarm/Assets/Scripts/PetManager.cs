using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TimeAgent))]
public class PetManager : MonoBehaviour
{
    [SerializeField] PetAIMove petAIMove;
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] ParticleSystem emotion;
    [SerializeField] GameObject imageCaution;
    [SerializeField] Item itemToSpawn;
    [SerializeField] int count;

    [SerializeField] float spread = 2f;
    [SerializeField] float probability = 0.5f;

    [SerializeField] TimeAgent thisTimeAgent;
    [SerializeField] int productTime;
    [SerializeField] PetData petData;

    private int hungryTime;

    bool grabed;

    private void Start()
    {
        thisTimeAgent.onTimeTick += Spawn;
        CalculateHungryTime();
        petData.sceneName = GameSceneManager.Instance.GetCurrentScene();
        if (petData.needeat == -1)
        {
            Hungry();
        }
        if (petData.inDark)
        {
            petAIMove.inDark = petData.inDark;
        }
    }

    private void OnDestroy()
    {
        //petData.needeat = needeat;
        //petData.countup = countup;
        petData.position = transform.position;
        //petData.sceneName = GameSceneManager.Instance.GetCurrentScene();
        //petData.active = active;
    }

    public void SetData(PetData data)
    {
        petData = data;
    }

    public void Spawn()
    {  
        if (grabed)
        {
            return;
        }

        if (CheckNightTime())
        {
            petData.inDark = PetInDark();
            if (petData.inDark)
            {
                return;
            }
        }
        else
        {
            petData.inDark = false;
            petAIMove.inDark = false;
        }

        petData.countup += (1 + petData.needeat);
        if (petData.countup != productTime)
        {
            if (petData.countup == hungryTime)
            {
                Hungry();
            }
            return;
        }


        if (UnityEngine.Random.value < probability)
        {
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            position.z = 0.1f;

            GameObject orgItem = ItemSpawnManager.Instance.pickUpItemPrefab;
            if (itemToSpawn.itemPrefab != null)
            {
                ItemSpawnManager.Instance.pickUpItemPrefab = itemToSpawn.itemPrefab;
            }
            ItemSpawnManager.Instance.SpawnItem(position, itemToSpawn, count);
            ItemSpawnManager.Instance.pickUpItemPrefab = orgItem;
        }
        petData.countup = 0;

    }

    internal void Warmed()
    {
        petData.inDark = false;
        petAIMove.inDark = false;
    }

    private bool CheckNightTime()
    {
        float nightvalue = GamesManager.Instance.dayTimeController.GetTimeCurve();
        return nightvalue > 0.5f;
    }

    private bool PetInDark()
    {
        return petAIMove.PetInDark();
    }

    public PetData GetData()
    {
        return petData;
    }

    private void CalculateHungryTime()
    {
        hungryTime = productTime / 2;
        //or sth else random.range(1,productTime-1) maybe
    }

    private void Hungry()
    {
        petData.needeat = -1;
        imageCaution.SetActive(true);
        petAIMove.hungry = true;
    }

    public void Grabed(bool value)
    {
        petAIMove.Grabed(value);
        grabed = value;
        petData.sceneName = GameSceneManager.Instance.GetCurrentScene();
        //petData.active = true;
    }

    public void ShowEmotion()
    {
        if (emotion == null) { return; }
        emotion.Play();
    }
    public void Eaten()
    {
        if (petData.needeat == -1)
        {
            CalculateHungryTime();
            petData.needeat = 0;
            ShowEmotion();
            imageCaution.SetActive(false);
        }
    }
}
