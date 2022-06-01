using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DirtTile
{
    public Vector3Int position;
    public int countTimeAlive;
    public int growed;
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

    internal void PlacePet(Item item, Vector3Int gridPosition)
    {
        if (tilemapBaseManager == null)
        {
            return;
        }

        tilemapBaseManager.PlacePet(item, gridPosition);
    }

    internal void ExtendDirtAlive(Vector3Int gridPosition)
    {
        if (tilemapBaseManager == null)
        {
            return;
        }

        tilemapBaseManager.ExtendDirtAlive(gridPosition);
    }

    internal void UpdateDirt(Vector3Int gridPosition)
    {
        if (tilemapBaseManager == null)
        {
            return;
        }

        tilemapBaseManager.UpdateDirt(gridPosition);
    }
}
