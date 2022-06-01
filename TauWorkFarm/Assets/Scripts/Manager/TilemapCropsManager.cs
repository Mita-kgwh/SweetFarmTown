using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap; // crop tilemap

    [SerializeField] GameObject cropsSpritePrefab;

    [SerializeField] CropsContainer cropsContainer;


    private void Start()
    {
        if (targetTilemap == null) { targetTilemap = GetComponent<Tilemap>(); }
        GamesManager.Instance.GetComponent<CropsManager>().tilemapCropsManager = this;
        onTimeTick += Tick;
        Init();
        VisualizeMap();
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < cropsContainer.crops.Count; i++)
        {
            VisualizeTile(cropsContainer.crops[i]);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < cropsContainer.crops.Count; i++)
        {
            cropsContainer.crops[i].renderer = null;
        }
    }

    public void Tick()
    {
        if (targetTilemap == null) { return; }

        int index = 0;

        while (index < cropsContainer.crops.Count)
        {
            Debug.Log(index + " " + cropsContainer.crops.Count);

            cropsContainer.crops[index].countTimeAlive += 1 + cropsContainer.crops[index].seeded;

            if (cropsContainer.crops[index].countTimeAlive >= 48) // 48 = half day
            {
                if (cropsContainer.crops[index].crop != null)
                {
                    cropsContainer.crops[index].Harvested();                  
                }
                targetTilemap.SetTile(cropsContainer.crops[index].position, null);
                GamesManager.Instance.tileMapReadController.baseManager.UpdateDirt(cropsContainer.crops[index].position);
                cropsContainer.crops.RemoveAt(index);
                continue;
            }

            if (cropsContainer.crops[index].crop == null) 
            {
                index++;
                continue;
            }

            if (cropsContainer.crops[index].isComplete)
            {
                Debug.Log("I'm done growing");
                cropsContainer.crops[index].seeded = 0;
                index++;
                continue;
            }

            cropsContainer.crops[index].growTimer += 1;

            if (cropsContainer.crops[index].growTimer >= cropsContainer.crops[index].crop.growthStageTime[cropsContainer.crops[index].growStage])
            {
                cropsContainer.crops[index].renderer.gameObject.SetActive(true);
                cropsContainer.crops[index].renderer.sprite = cropsContainer.crops[index].crop.sprites[cropsContainer.crops[index].growStage];

                cropsContainer.crops[index].growStage += 1;
            }
            index++;
        }


        //foreach (CropTile cropTile in cropsContainer.crops)
        //{
        //    if (cropTile.crop == null) { continue; }

        //    cropTile.damage += 0.05f;

        //    if (cropTile.damage >= 2f)
        //    {
        //        cropTile.Harvested();
        //        targetTilemap.SetTile(cropTile.position, null);
        //        continue;
        //    }

        //    if (cropTile.isComplete)
        //    {
        //        //Debug.Log("I'm done growing");
        //        continue;
        //    }

        //    cropTile.growTimer += 1;

        //    if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
        //    {
        //        cropTile.renderer.gameObject.SetActive(true);
        //        cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

        //        cropTile.growStage += 1;
        //    }
        //}
    }

    internal bool Check(Vector3Int position)
    {
        return cropsContainer.Get(position) != null;
    }

    public void Plow(Vector3Int position)
    {
        if (Check(position)) { return; }
        CreatePlowedTile(position);
    }

    public bool Seed(Vector3Int position, Crop toSeed)
    {
        //Debug.Log("alo alo");
        CropTile tile = cropsContainer.Get(position);

        if (tile == null) { return false; }

        if (tile.crop != null) { return false; }

        targetTilemap.SetTile(position, seeded);

        tile.crop = toSeed;
        tile.seeded = -1;

        return true;
    }

    public void VisualizeTile(CropTile cropTile)
    {
        targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded: plowed);

        if (cropTile.renderer == null)
        {
            GameObject gobj = Instantiate(cropsSpritePrefab, transform);
            gobj.transform.position = targetTilemap.CellToWorld(cropTile.position);
            gobj.transform.position -= Vector3.forward * 0.01f;
            //gobj.SetActive(false);
            cropTile.renderer = gobj.GetComponent<SpriteRenderer>();
        }

        bool isGrowing =
            (cropTile.crop != null) &&
            (cropTile.growTimer >= cropTile.crop.growthStageTime[0]);

        cropTile.renderer.gameObject.SetActive(isGrowing);
        if (isGrowing)
        {
            cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage - 1];
        }
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        cropsContainer.Add(crop);

        crop.position = position;
        VisualizeTile(crop);

        //targetTilemap.SetTile(position, plowed);

    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        CropTile cropTile = cropsContainer.Get(gridPosition);
        if (cropTile == null) { return;}

        if (cropTile.isComplete)
        {
            ItemSpawnManager.Instance.SpawnItem(
                targetTilemap.CellToWorld(gridPosition),
                cropTile.crop.yield,
                cropTile.crop.count);

            cropTile.Harvested();
            VisualizeTile(cropTile);
        }
    }

}
