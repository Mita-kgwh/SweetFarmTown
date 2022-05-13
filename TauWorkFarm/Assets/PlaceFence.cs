using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Fence")]
public class PlaceFence : ToolsAction
{
    [SerializeField] List<TileBase> canPlaceFences;
    [SerializeField] AudioClip onFenceUsed;
    public override bool OnApplyToTileMap(Vector3Int gridPosition,
        TileMapReadController tileMapReadController,
        Item item
        )
    {
        TileBase tileToFence = tileMapReadController.GetTileBase(gridPosition);

        if (!canPlaceFences.Contains(tileToFence))
        {
            return false;
        }

        if (tileMapReadController.groundManager.CheckEmpty(gridPosition))
        {
            tileMapReadController.groundManager.PlaceFence(gridPosition);

            MusicManager.Instance.PlayEfx(onFenceUsed);

            return true;
        }

        return false;
    }

    public override void OnItemUsed(Item useItem, ItemContainer toolbar)
    {
        toolbar.Remove(useItem);
    }
}
