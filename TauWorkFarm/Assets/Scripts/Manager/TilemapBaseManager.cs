using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBaseManager : TimeAgent
{
    [SerializeField] TileBase dirted;

    [SerializeField] TileBase normal;

    [SerializeField] Tilemap baseTilemap;

    [SerializeField] int countToNormal;

    [SerializeField] DirtsContainer dirtsContainer;
    void Start()
    {
        if (baseTilemap == null) { baseTilemap = GetComponent<Tilemap>(); }
        GamesManager.Instance.GetComponent<BaseManager>().tilemapBaseManager = this;
        onTimeTick += TickCount;
        Init();
        VisualizeMap();
    }

    public void TickCount()
    {
        int i = 0;
        while (i < dirtsContainer.dirts.Count)
        {
            if (dirtsContainer.dirts[i].growed > 0)
            {
                i++;
                continue;
            }
            dirtsContainer.dirts[i].countTimeAlive += 1;
            if (dirtsContainer.dirts[i].countTimeAlive == countToNormal)
            {
                baseTilemap.SetTile(dirtsContainer.dirts[i].position, normal);
                dirtsContainer.dirts.Remove(dirtsContainer.dirts[i]);
            }
            else
            {
                i++;
            }
        }
    }

    private void VisualizeMap()
    {
        if (dirtsContainer == null)
        {
            return;
        }
        for (int i = 0; i < dirtsContainer.dirts.Count; i++)
        {
            VisualizeTile(dirtsContainer.dirts[i]);
        }
    }

    internal void UpdateDirt(Vector3Int gridPosition)
    {
        if (dirtsContainer == null)
        {
            return;
        }
        DirtTile dirtToUpdate;
        Vector3Int positionCheck = new Vector3Int();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                positionCheck.x = gridPosition.x + i;
                positionCheck.y = gridPosition.y + j;

                dirtToUpdate = dirtsContainer.Get(positionCheck);

                dirtToUpdate.growed -= 1;      
            }
    }

    private void VisualizeTile(DirtTile dirtTile)
    {
        if (dirtsContainer == null)
        {
            return;
        }
        baseTilemap.SetTile(dirtTile.position, dirted);
    }

    internal void MakeDirt(Vector3Int gridPosition)
    {
        if (dirtsContainer == null)
        {
            return;
        }
        DirtTile dirtTile = new DirtTile();
        dirtTile.position = gridPosition;

        dirtsContainer.Add(dirtTile);
        VisualizeTile(dirtTile);
    }

    internal void ExtendDirtAlive(Vector3Int gridPosition)
    {
        if (dirtsContainer == null)
        {
            return;
        }
        DirtTile dirtTile = dirtsContainer.Get(gridPosition);
        dirtTile.countTimeAlive = 0;
        dirtTile.growed += 1;
    }

    internal void PlacePet(Item item, Vector3Int gridPosition)
    {
        GameObject pet = Instantiate(item.itemPrefab, gridPosition, Quaternion.identity);
        PlayerPetManager.Instance.AddPet(pet.GetComponent<PetManager>().GetData());
    }

    internal bool CanDirt(Vector3Int gridPosition)
    {
        if (dirtsContainer == null)
        {
            return false;
        }
        return dirtsContainer.Get(gridPosition) == null;
    }
}
