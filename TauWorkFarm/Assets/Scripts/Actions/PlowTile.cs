using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow Tile")]
public class PlowTile : ToolsAction
{
    [SerializeField] List<TileBase> canPlow;
    //[SerializeField] RuleTile canPlowRuleTile;
    [SerializeField] AudioClip onPlowUsed;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, 
        TileMapReadController tileMapReadController,
        Item item)
    {
        Debug.Log("OnApplyToTileMap");
        if (canPlow == null)
        {
            Debug.Log("canPlow null");
            return true;
        }
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);
        if (tileToPlow == null)
        {
            Debug.Log("tile to plow null");
        }
        //else
        //{
        //    Debug.Log(tileToPlow.name);
        //}
        if (!canPlow.Contains(tileToPlow))
        {
            Debug.Log("not contains");
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);

        MusicManager.Instance.PlayEfx(onPlowUsed);

        return true;
    }
}
