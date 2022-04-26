using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeAgent))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Item itemToSpawn;
    [SerializeField] int count;

    [SerializeField] float spread = 2f;
    [SerializeField] float probability = 0.5f;

    [SerializeField] TimeAgent thisTimeAgent;

    // now ItemSpwner is a time dependent component

    private void Start()
    {
        thisTimeAgent.onTimeTick += Spawn;
    }

    void Spawn()
    {
        if (UnityEngine.Random.value < probability)
        {
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
             
            ItemSpawnManager.Instance.SpawnItem(position, itemToSpawn, count);
        }
            
    }
}
