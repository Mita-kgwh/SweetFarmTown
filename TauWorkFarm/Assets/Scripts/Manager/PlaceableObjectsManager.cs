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

    public void Place(Item item, Vector3Int positionOnGrid)
    {
        //Debug.Log("ALo");
        GameObject gobj = Instantiate(item.itemPrefab);
        Vector3 position = targetTilemap.CellToWorld(positionOnGrid) + targetTilemap.cellSize/2;
        gobj.transform.position = position;
        placeableObjsContainer.placeableObjects.Add(new PlaceableObject(item, gobj.transform, positionOnGrid));
    }
}
