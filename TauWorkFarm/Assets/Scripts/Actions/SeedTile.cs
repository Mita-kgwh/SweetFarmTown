using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
public class SeedTile : ToolsAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, 
        TileMapReadController tileMapReadController,
        Item item
        )
    {
        if (tileMapReadController.cropsManager.Check(gridPosition) == false)
        {
            return false;
        }
        
        return tileMapReadController.cropsManager.Seed(gridPosition, item.crop);
    }

    public override void OnItemUsed(Item useItem, ItemContainer toolbar)
    {
        toolbar.Remove(useItem); 
    }
}
