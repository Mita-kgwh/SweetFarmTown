using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//manage object place in the scene so we can load obj when load scene
public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjsContainer;
    [SerializeField] Tilemap targetTilemap; // base tilemap

    private void Start()
    {
        GamesManager.Instance.GetComponent<PlaceableObjectReferenceManager>().placeableObjectsManager = this;
        VisualizaMap();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < placeableObjsContainer.placeableObjects.Count; i++)
        {
            if (placeableObjsContainer.placeableObjects[i].targetObject == null) { continue; }

            IPersistant persistant = placeableObjsContainer.placeableObjects[i].targetObject.GetComponent<IPersistant>();
            if (persistant != null)
            {
                string jsonString = persistant.Read(); //data sau khi game thuc thi, dc chuyen lai thanh json, gan ra jsonstring de luu vao container
                placeableObjsContainer.placeableObjects[i].objectState = jsonString;
            }


            placeableObjsContainer.placeableObjects[i].targetObject = null;
        }
    }

    private void VisualizaMap()
    {
        for (int i = 0; i < placeableObjsContainer.placeableObjects.Count; i++)
        {
            VisualizaItem(placeableObjsContainer.placeableObjects[i]);
        }
    }

    private void VisualizaItem(PlaceableObject placeableObject)
    {
        GameObject gobj = Instantiate(placeableObject.placedItem.itemPrefab);
        gobj.transform.parent = transform;

        Vector3 position = 
            targetTilemap.CellToWorld(placeableObject.positionOnGrid) 
            + targetTilemap.cellSize / 2;

        position -= Vector3.forward * 0.1f;
        gobj.transform.position = position;

        IPersistant persistant = gobj.GetComponent<IPersistant>();
        if (persistant != null)
        {
            persistant.Load(placeableObject.objectState); //lay jsonstring gan vao data cua placeobj trc khi dat xuong
        }

        placeableObject.targetObject = gobj.transform;
    }

    public bool Check(Vector3Int position)
    {
        return placeableObjsContainer.Get(position) != null;
    }

    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if (Check(positionOnGrid)) { return; }
        PlaceableObject placeableObject = new PlaceableObject(item, positionOnGrid);
        VisualizaItem(placeableObject);
        placeableObjsContainer.placeableObjects.Add(placeableObject);
    }
    internal void PickUp(Vector3Int gridPosition)
    {
        PlaceableObject placedObject = placeableObjsContainer.Get(gridPosition);
        if (placedObject == null)
        {
            return;
        }
        ItemSpawnManager.Instance.SpawnItem(
            targetTilemap.CellToWorld(gridPosition), 
            placedObject.placedItem, 
            1);

        Destroy(placedObject.targetObject.gameObject); //destroy obj on scene

        placeableObjsContainer.Remove(placedObject);
    }
}
