using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float spawnArea_height = 1f;
    [SerializeField] float spawnArea_width = 1f;

    [SerializeField] GameObject[] spawnObject;
    int length;
    [SerializeField] float probability = 0.1f;
    [SerializeField] int spawnCount = 1;

    [SerializeField] bool oneTime = false;

    private void Start()
    {
        length = spawnObject.Length;

        if (!oneTime)
        {
            TimeAgent timeAgent = GetComponent<TimeAgent>();
            timeAgent.onTimeTick += Spawn;
        }
        else
        {
            Spawn();
            Destroy(gameObject);
        }
        
    }

    private void Spawn()
    {
        if (Random.value > probability) { return; }

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject gobj = Instantiate(spawnObject[Random.Range(0, length)]);
            Transform tf = gobj.transform;

            Vector3 position = transform.position;
            position.x += Random.Range(-spawnArea_width, spawnArea_width);
            position.y += Random.Range(-spawnArea_height, spawnArea_height);

            tf.position = position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2, spawnArea_height * 2));
    }                                                           
}
