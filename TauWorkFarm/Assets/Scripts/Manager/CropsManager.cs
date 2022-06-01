using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class CropTile
{
    // store info about the crops and plowed tile in scene
    public int growTimer; //store current stage, vv
    public int growStage;
    public Crop crop; // store base info such as how many stage, how long to grow
    public SpriteRenderer renderer;
    public int seeded;
    public Vector3Int position;
    public int countTimeAlive;

    internal void Clear()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer = null;
        seeded = 0;
        position = Vector3Int.zero;
        countTimeAlive = 0;
    }

    public bool isComplete
    {
        get
        {
            if (crop == null ) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        seeded = 0;
    }
}
public class CropsManager : MonoBehaviour
{
    //this responsible for communication between player and cropgrid
    public TilemapCropsManager tilemapCropsManager;

    public void PickUp(Vector3Int position)
    {
        if (tilemapCropsManager == null)
        {
            Debug.LogWarning("No tilemapcropmanager are referenced in the crops manager");
            return;
        }
        tilemapCropsManager.PickUp(position);
    }
    
    public bool Check(Vector3Int position)
    {
        if (tilemapCropsManager == null)
        {
            Debug.LogWarning("No tilemapcropmanager are referenced in the crops manager");
            return false;
        }

        return tilemapCropsManager.Check(position);
    }

    public bool Seed(Vector3Int position, Crop toSeed)
    {
        if (tilemapCropsManager == null)
        {
            Debug.LogWarning("No tilemapcropmanager are referenced in the crops manager");
            return false;
        }

        return tilemapCropsManager.Seed(position, toSeed);
    }

    public void Plow(Vector3Int position)
    {
        if (tilemapCropsManager == null)
        {
            Debug.LogWarning("No tilemapcropmanager are referenced in the crops manager");
            return;
        }

        tilemapCropsManager.Plow(position);
    }

}
