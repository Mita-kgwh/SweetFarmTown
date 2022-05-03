using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//manage object place in the scene so we can load obj when load scene
public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjsContainer;
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        VisualizaMap();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < placeableObjsContainer.placeableObjects.Count; i++)
        {
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
        Vector3 position = 
            targetTilemap.CellToWorld(placeableObject.positionOnGrid) 
            + targetTilemap.cellSize / 2;

        position -= Vector3.forward * 0.1f;
        gobj.transform.position = position;

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

        Destroy(placedObject.targetObject.gameObject);

        placeableObjsContainer.Remove(placedObject);
    }
}
