using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    // store info about the crops and plowed tile in scene
    public int growTimer; //store current stage, vv
    public int growStage;
    public Crop crop; // store base info such as how many stage, how long to grow
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;

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
        damage = 0;
    }
}
public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject cropsSpritePrefab;

    //list of all crops in scene, when u want to interact
    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();

    }

    public void Tick()
    {
        foreach (CropTile cropTile in crops.Values)
        {
            if (cropTile.crop == null) { continue; }

            cropTile.damage += 0.05f;

            if (cropTile.damage >= 2f)
            {
                cropTile.Harvested();
                targetTilemap.SetTile(cropTile.position, plowed);
                continue;
            }

            if (cropTile.isComplete)
            {
                Debug.Log("I'm done growing");
                continue;
            }

            cropTile.growTimer += 1;

            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                cropTile.growStage += 1;
            }
 
        }
    }

    public bool CheckIsPlowed(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
        {
            //this tile have croped already  
            return;
        }
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        targetTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        GameObject gobj = Instantiate(cropsSpritePrefab);
        gobj.transform.position = targetTilemap.CellToWorld(position);
        gobj.transform.position -= Vector3.forward * 0.01f;
        gobj.SetActive(false);
        crop.renderer = gobj.GetComponent<SpriteRenderer>();

        crop.position = position;
        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        if (!crops.ContainsKey(position))
        {
            return;
        }

        CropTile cropTile = crops[position];
        if (cropTile.isComplete)
        {
            ItemSpawnManager.Instance.SpawnItem(
                targetTilemap.CellToWorld(gridPosition),
                cropTile.crop.yield,
                cropTile.crop.count);

            targetTilemap.SetTile(gridPosition, plowed);
            cropTile.Harvested();
        }
    }
}
