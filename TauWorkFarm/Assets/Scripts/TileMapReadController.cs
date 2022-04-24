using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            //trong moi tileData co list cua cac tile nen ta lai phai duyet qua lan nua
            foreach (TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }


    public Vector3Int GetGridPosition(Vector2 mousePosition, bool isMousePosition)
    {
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
        //va lay duoc tile do ra de check data
        TileBase tile = tilemap.GetTile(gridPosition);

        //Debug.Log("Tile in position = " + gridPosition + " is " + tile);

        return tile;
    }

    public TileData GetTileData(TileBase _tileBase)
    {
        return dataFromTiles[_tileBase];
    }

}
