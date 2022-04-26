using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow Tile")]
public class PlowTile : ToolsAction
{
    [SerializeField] List<TileBase> canPlow;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        Debug.Log("OnApplyToTileMap");
        if (canPlow == null)
        {
            Debug.Log("canPlow null");
            return true;
        }
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);
        if (!canPlow.Contains(tileToPlow))
        {
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);

        return true;
    }
}
