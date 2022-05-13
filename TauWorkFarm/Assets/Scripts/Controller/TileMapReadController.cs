using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap baseTilemap;
    public CropsManager cropsManager;
    public GroundManager groundManager;
    public PlaceableObjectReferenceManager objectsManager;


    public Vector3Int GetGridPosition(Vector2 mousePosition, bool isMousePosition)
    {
        if (baseTilemap == null)
        {
            baseTilemap = GameObject.Find("Base").GetComponent<Tilemap>();
        }
        if (baseTilemap == null) { return Vector3Int.zero; }

        Vector3 worldPosition;

        if (isMousePosition)
        {
            // chuyen tu mouse position tren scene sang camera
            worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        else
        {
            worldPosition = mousePosition;
        }

        // tu do chuyen ve grid cua tilemap
        Vector3Int gridPosition = baseTilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        if (baseTilemap == null)
        {
            baseTilemap = GameObject.Find("Base").GetComponent<Tilemap>();
        }
        if (baseTilemap == null) { return null; }

        //va lay duoc tile do ra de check data
        TileBase tile = baseTilemap.GetTile(gridPosition);

        //Debug.Log("Tile in position = " + gridPosition + " is " + tile);

        return tile;
    }

}
