using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Gate")]
public class PlaceGate : ToolsAction
{
    [SerializeField] List<TileBase> canPlaceGates;
    [SerializeField] AudioClip onGateUsed;
    public override bool OnApplyToTileMap(Vector3Int gridPosition,
        TileMapReadController tileMapReadController,
        Item item
        )
    {
        TileBase tileToGated = tileMapReadController.GetTileBase(gridPosition);

        if (!canPlaceGates.Contains(tileToGated))
        {
            return false;
        }

        //if (tileMapReadController.groundManager.CheckEmpty(gridPosition))
        {
            tileMapReadController.groundManager.PlaceGate(gridPosition);

            MusicManager.Instance.PlayEfx(onGateUsed);

            return true;
        }

        //return false;
    }

    public override void OnItemUsed(Item useItem, ItemContainer toolbar)
    {
        toolbar.Remove(useItem);
    }
}
