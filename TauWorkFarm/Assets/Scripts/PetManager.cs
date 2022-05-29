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
    }
    private void OnDestroy()
    {
        //petData.needeat = needeat;
        //petData.countup = countup;
        petData.position = transform.position;
        petData.sceneName = SceneManager.GetActiveScene().name;
        //petData.active = active;
    }

    public void SetData(PetData data)
    {
        petData = data;
    }

    public void Spawn()
    {
        if (grabed)// || petData.active == false)
        {
            return;
        }
        petData.countup += (1 + petData.needeat);
        if (petData.countup != productTime)
        {
            if (petData.countup == hungryTime)
            {
                //Debug.Log("Hurry");
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

            //ItemSpawnManager.Instance.pickUpItemPrefab = prefabToSpawn;
            ItemSpawnManager.Instance.SpawnItem(position, itemToSpawn, count);
        }
        petData.countup = 0;

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
