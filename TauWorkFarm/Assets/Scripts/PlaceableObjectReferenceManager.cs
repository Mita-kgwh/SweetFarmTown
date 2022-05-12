using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectReferenceManager : MonoBehaviour
{
    public PlaceableObjectsManager placeableObjectsManager;

    public void Place(Item item, Vector3Int pos)
    {
        if (placeableObjectsManager == null)
        {
            Debug.LogWarning("No placeableObjManager refence detected");
            return;
        }

        placeableObjectsManager.Place(item, pos);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (placeableObjectsManager == null)
        {
            Debug.LogWarning("No placeableObjManager refence detected");
            return;
        }
        placeableObjectsManager.PickUp(gridPosition);
    }

    public bool Check(Vector3Int gridPosition)
    {
        if (placeableObjectsManager == null)
        {
            Debug.LogWarning("No placeableObjManager refence detected");
            return false;
        }

        return placeableObjectsManager.Check(gridPosition);
    }
}
