using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGroundManager : MonoBehaviour
{
    [SerializeField] TileBase fenced;

    [SerializeField] TileBase gated;

    [SerializeField] Tilemap groundTilemap; // ground tilemap

    //[SerializeField] GameObject fencesSpritePrefab;

    [SerializeField] FencesContainer fencesContainer;

    public Tilemap GetTilemap()
    {
        return groundTilemap;
    }

    internal void OpenGate(Vector3Int gridPosition)
    {
        groundTilemap.SetColliderType(gridPosition, Tile.ColliderType.None);
    }

    internal void CloseGate(Vector3Int gridPosition)
    {
        groundTilemap.SetColliderType(gridPosition, Tile.ColliderType.Sprite);
    }

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
        groundTilemap.SetTile(fenceTile.position, fenceTile.isGate? gated : fenced);
        if (fenceTile.isGate)
        {
            groundTilemap.SetColor(fenceTile.position, Color.red);
        }
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
        fenceTile.isGate = false;
        VisualizeTile(fenceTile);
    }

    public void PlaceGate(Vector3Int gridPosition)
    {
        if (!CheckEmpty(gridPosition))
        {
            fencesContainer.Remove(fencesContainer.Get(gridPosition));
        }
        FenceTile fenceTile = new FenceTile();
        fencesContainer.Add(fenceTile);

        fenceTile.position = gridPosition;
        fenceTile.isGate = true;
        VisualizeTile(fenceTile);
    }

    public bool DestroyFenced(Vector3Int gridPosition)
    {
        if (!CheckEmpty(gridPosition))
        {
            fencesContainer.Remove(fencesContainer.Get(gridPosition));
            groundTilemap.SetTile(gridPosition, null);
            return true;
        }
        return false;
    }
}
