using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeAgent))]
public class ItemSpawner : MonoBehaviour
{
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
    private int needcare = 0;

    // now ItemSpwner is a time dependent component

    private void Start()
    {
        thisTimeAgent.onTimeTick += Spawn;
        imageCaution.SetActive(false);
    }

    public void Spawn()
    {
        countup += (1 + needcare);
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

            ItemSpawnManager.Instance.pickUpItemPrefab = prefabToSpawn;
            ItemSpawnManager.Instance.SpawnItem(position, itemToSpawn, count);
        }
        countup = 0;

    }
    private void Hungry()
    {
        needcare = -1;
        imageCaution.SetActive(true);

    }

    public void ShowEmotion()
    {
        if (emotion == null) { return; }
        emotion.Play();
    }
    private void OnMouseDown()
    {
        Debug.Log("On mouse down");
        if (needcare == -1)
        {
            needcare = 0;
            ShowEmotion();
            imageCaution.SetActive(false);
        }
    }
}
