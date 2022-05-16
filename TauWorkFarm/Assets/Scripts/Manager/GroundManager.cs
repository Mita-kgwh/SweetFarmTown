using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FenceTile
{
    public Vector3Int position;
}

public class GroundManager : MonoBehaviour
{
    public TilemapGroundManager tilemapGroundManager;

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
}
