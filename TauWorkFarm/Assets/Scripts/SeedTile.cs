using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
public class SeedTile : ToolsAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        if (tileMapReadController.cropsManager.CheckIsPlowed(gridPosition) == false)
        {
            return false;
        }
        
        tileMapReadController.cropsManager.Seed(gridPosition);

        return true;
    }

    public override void OnItemUsed(Item useItem, ItemContainer toolbar)
    {
        toolbar.Remove(useItem);
    }
}
