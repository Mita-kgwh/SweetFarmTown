using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int countup = -1;
    private int needeat = 0;

    //FoodTrough foodTrough;

    bool grabed;


    private void Start()
    {

        thisTimeAgent.onTimeTick += Spawn;
        imageCaution.SetActive(false);
    }

    public void Spawn()
    {
        if (grabed)
        {
            return;
        }
        countup += (1 + needeat);
        if (countup != productTime)
        {
            if (countup == (productTime / 2))
            {
                Debug.Log("Hurry");
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
        countup = 0;

    }

    private void Hungry()
    {
        needeat = -1;
        imageCaution.SetActive(true);
        petAIMove.hungry = true;
    }

    public void Grabed(bool value)
    {
        petAIMove.Grabed(value);
        grabed = value;
    }

    public void ShowEmotion()
    {
        if (emotion == null) { return; }
        emotion.Play();
    }
    public void Eaten()
    {
        if (needeat == -1)
        {
            needeat = 0;
            ShowEmotion();
            imageCaution.SetActive(false);
        }
    }
}
