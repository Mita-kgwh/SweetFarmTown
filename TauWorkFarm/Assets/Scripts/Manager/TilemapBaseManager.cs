using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBaseManager : MonoBehaviour
{
    [SerializeField] TileBase dirted;

    [SerializeField] Tilemap baseTilemap; 

    [SerializeField] DirtsContainer dirtsContainer;
    void Start()
    {
        if (baseTilemap == null) { baseTilemap = GetComponent<Tilemap>(); }
        GamesManager.Instance.GetComponent<BaseManager>().tilemapBaseManager = this;
        VisualizeMap();
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < dirtsContainer.dirts.Count; i++)
        {
            VisualizeTile(dirtsContainer.dirts[i]);
        }
    }

    private void VisualizeTile(DirtTile dirtTile)
    {
        baseTilemap.SetTile(dirtTile.position, dirted);
    }

    internal void MakeDirt(Vector3Int gridPosition)
    {
        DirtTile dirtTile = new DirtTile();
        dirtTile.position = gridPosition;

        dirtsContainer.Add(dirtTile);
        VisualizeTile(dirtTile);
    }
    internal bool CanDirt(Vector3Int gridPosition)
    {
        return dirtsContainer.Get(gridPosition) == null;
    }
}
