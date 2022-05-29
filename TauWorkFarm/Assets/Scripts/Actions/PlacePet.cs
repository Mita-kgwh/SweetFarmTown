using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Pet")]
public class PlacePet : ToolsAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        if (tileMapReadController.objectsManager.Check(gridPosition))
        {
            //have obj
            return false; //not allow to place new obj on this tile
        }

        tileMapReadController.baseManager.PlacePet(item, gridPosition);

        return true;
    }

    public override void OnItemUsed(Item useItem, ItemContainer toolbar)
    {
        toolbar.Remove(useItem);
    }
}
