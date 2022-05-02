using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;

    [SerializeField] GameObject cropsSpritePrefab;

    [SerializeField] CropsContainer cropsContainer;


    private void Start()
    {
        GamesManager.Instance.GetComponent<CropsManager>().tilemapCropsManager = this;
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        if (targetTilemap == null) { return; }

        foreach (CropTile cropTile in cropsContainer.crops)
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

    internal bool Check(Vector3Int position)
    {
        return cropsContainer.Get(position) != null;
    }

    //list of all crops in scene, when u want to interact
    //Dictionary<Vector2Int, CropTile> crops;

    //public bool CheckIsPlowed(Vector3Int position)
    //{
    //    return crops.ContainsKey((Vector2Int)position);
    //}

    public void Plow(Vector3Int position)
    {
       
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = cropsContainer.Get(position);

        if (tile == null)
        {
            return;
        }

        targetTilemap.SetTile(position, seeded);

        tile.crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        cropsContainer.Add(crop);

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
        CropTile cropTile = cropsContainer.Get(gridPosition);
        if (cropTile == null)
        {
            return;
        }

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
