using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DirtTile
{
    public Vector3Int position;
}
public class BaseManager : MonoBehaviour
{
    public TilemapBaseManager tilemapBaseManager;

    internal void MakeDirt(Vector3Int gridPosition)
    {
        if (tilemapBaseManager == null)
        {
            return;
        }

        tilemapBaseManager.MakeDirt(gridPosition);
    }

    internal bool CanDirt(Vector3Int gridPosition)
    {
        if (tilemapBaseManager == null)
        {
            return false; 
        }

        return tilemapBaseManager.CanDirt(gridPosition);
    }
}