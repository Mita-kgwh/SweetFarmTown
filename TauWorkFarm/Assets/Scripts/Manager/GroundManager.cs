using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FenceTile
{
    public Vector3Int position;
    public bool isGate;
}

public class GroundManager : MonoBehaviour
{
    public TilemapGroundManager tilemapGroundManager;

    internal void OpenGate(Vector3 position)
    {
        Vector3Int gridPosition = new Vector3Int((int)position.x, (int)position.y, 0);
        tilemapGroundManager.OpenGate(gridPosition);
    }

    internal void CloseGate(Vector3 position)
    {
        Vector3Int gridPosition = new Vector3Int((int)position.x, (int)position.y, 0);
        tilemapGroundManager.CloseGate(gridPosition);
    }

    internal bool CheckEmpty(Vector3Int gridPosition)
    {
        if (tilemapGroundManager == null)
        {
            return false;
        }

        return tilemapGroundManager.CheckEmpty(gridPosition);
    }

    internal void PlaceFence(Vector3Int gridPosition)
    {
        if (tilemapGroundManager == null)
        {
            return;
        }

        tilemapGroundManager.PlaceFence(gridPosition);
    }

    internal void PlaceGate(Vector3Int gridPosition)
    {
        if (tilemapGroundManager == null)
        {
            return;
        }

        tilemapGroundManager.PlaceGate(gridPosition);
    }
}
