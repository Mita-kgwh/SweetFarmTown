using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public CropsManager cropsManager;
    public PlaceableObjectReferenceManager objectsManager;


    public Vector3Int GetGridPosition(Vector2 mousePosition, bool isMousePosition)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("Base").GetComponent<Tilemap>();
        }
        if (tilemap == null) { return Vector3Int.zero; }

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
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("Base").GetComponent<Tilemap>();
        }
        if (tilemap == null) { return null; }

        //va lay duoc tile do ra de check data
        TileBase tile = tilemap.GetTile(gridPosition);

        //Debug.Log("Tile in position = " + gridPosition + " is " + tile);

        return tile;
    }

}
