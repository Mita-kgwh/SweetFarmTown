using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow Tile")]
public class PlowTile : ToolsAction
{
    [SerializeField] List<TileBase> canPlow;
    [SerializeField] AudioClip onPlowUsed;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, 
        TileMapReadController tileMapReadController,
        Item item)
    {
        //Debug.Log("OnApplyToTileMap");
        if (canPlow == null)
        {
            Debug.Log("canPlow null");
            return true;
        }
        //check if that tile is center
        TileBase tileToPlow;
        Vector3Int positionCheck = new Vector3Int();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                positionCheck.x = gridPosition.x + i;
                positionCheck.y = gridPosition.y + j;

                tileToPlow = tileMapReadController.GetTileBase(positionCheck);
                if (!canPlow.Contains(tileToPlow))
                {
                    //Debug.Log("not contains");
                    return false;
                }
            }

        tileMapReadController.cropsManager.Plow(gridPosition);

        MusicManager.Instance.PlayEfx(onPlowUsed);

        return true;
    }
}
