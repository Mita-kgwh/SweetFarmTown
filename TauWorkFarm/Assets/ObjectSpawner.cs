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
    [SerializeField] int objectSpawnLimit = -1;

    [SerializeField] bool oneTime = false;

    List<SpawnedObject> spawnedObjects;
    [SerializeField] JSONStringList targetSaveJSONList;
    [SerializeField] int idInList = -1;

    private void Start()
    {
        length = spawnObject.Length;

        if (!oneTime)
        {
            TimeAgent timeAgent = GetComponent<TimeAgent>();
            timeAgent.onTimeTick += Spawn;
            spawnedObjects = new List<SpawnedObject>();

            LoadData();
        }
        else
        {
            Spawn();
            //Destroy(gameObject);
        }
        
    }

    public void SpawnedObjectDestroyed(SpawnedObject _spawnedObject)
    {
        spawnedObjects.Remove(_spawnedObject);
    }

    private void Spawn()
    {
        if (Random.value > probability) { return; }
        if (spawnedObjects != null)
        {
            if (objectSpawnLimit <= spawnedObjects.Count && objectSpawnLimit != -1) { return; }

        }
        for (int i = 0; i < spawnCount; i++)
        {
            int id = Random.Range(0, length);
            GameObject gobj = Instantiate(spawnObject[id]);
            Transform tf = gobj.transform;

            tf.SetParent(transform);

            if (!oneTime)
            {            
                SpawnedObject _spawnedObject = gobj.AddComponent<SpawnedObject>();
                spawnedObjects.Add(_spawnedObject);
                _spawnedObject.objId = id;
            }

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

    public class ToSave
    {
        public List<SpawnedObject.SaveSpawnedObjectData> spawnedObjectDatas;

        public ToSave()
        {
            spawnedObjectDatas = new List<SpawnedObject.SaveSpawnedObjectData>();
        }
    }
    public string Read()
    {
        ToSave toSave = new ToSave();

        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            toSave.spawnedObjectDatas.Add(
                new SpawnedObject.SaveSpawnedObjectData(
                    spawnedObjects[i].objId, 
                    spawnedObjects[i].transform.position)
                );
        }

        return JsonUtility.ToJson(toSave);
    }

    public void Load(string jsonString)
    {
        if (jsonString == "" || jsonString == "{}" || jsonString == null) { return; }

        ToSave toLoad = JsonUtility.FromJson<ToSave>(jsonString);

        for (int i = 0; i < toLoad.spawnedObjectDatas.Count; i++)
        {
            SpawnedObject.SaveSpawnedObjectData data = toLoad.spawnedObjectDatas[i];
            GameObject gobj = Instantiate(spawnObject[data.objectId]);
            gobj.transform.position = data.worldPosition;
            gobj.transform.SetParent(transform);
            SpawnedObject sobj = gobj.AddComponent<SpawnedObject>();
            sobj.objId = data.objectId;
            spawnedObjects.Add(sobj);
        }

    }

    private void OnDestroy()
    {
        if (oneTime) { return; }
        SaveData();
    }

    private void SaveData()
    {
        if (CheckJSON() == false) { return; }

        string jsonString = Read();
        targetSaveJSONList.SetString(jsonString, idInList);
    }

    private void LoadData()
    {
        if (CheckJSON() == false) { return; }

        Load(targetSaveJSONList.GetString(idInList));
    }

    private bool CheckJSON()
    {
        if (oneTime) { return false; }
        if (targetSaveJSONList == null)
        {
            Debug.LogError("save jsonlist null");
            return false;
        }
        if (idInList == -1)
        {
            Debug.LogError("idInList not assigned, data can't save");
            return false;
        }
        return true;
    }

}
