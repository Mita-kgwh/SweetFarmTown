using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGroundManager : MonoBehaviour
{
    [SerializeField] TileBase fenced;

    [SerializeField] Tilemap groundTilemap; // ground tilemap

    //[SerializeField] GameObject fencesSpritePrefab;

    [SerializeField] FencesContainer fencesContainer;

    void Start()
    {
        if (groundTilemap == null) { groundTilemap = GetComponent<Tilemap>(); }
        GamesManager.Instance.GetComponent<GroundManager>().tilemapGroundManager = this;
        VisualizeMap();
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < fencesContainer.fences.Count; i++)
        {
            VisualizeTile(fencesContainer.fences[i]);
        }
    }

    private void VisualizeTile(FenceTile fenceTile)
    {
        groundTilemap.SetTile(fenceTile.position, fenced);
    }
    internal bool CheckEmpty(Vector3Int gridPosition)
    {
        return fencesContainer.Get(gridPosition) == null;
    }

    internal void PlaceFence(Vector3Int gridPosition)
    {
        FenceTile fenceTile = new FenceTile();
        fencesContainer.Add(fenceTile);

        fenceTile.position = gridPosition;
        VisualizeTile(fenceTile);
    }
}
