using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    private static ItemSpawnManager instance;
    public static ItemSpawnManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ItemSpawnManager>();
            return instance;
        }
    }

    [SerializeField] GameObject pickUpItemPrefab;

    public void SpawnItem(Vector3 position, Item _item, int _count)
    {
        GameObject spawnObject = Instantiate(pickUpItemPrefab, position, Quaternion.identity);
        spawnObject.GetComponent<Collectable>().Set(_item, _count);
    }
}
