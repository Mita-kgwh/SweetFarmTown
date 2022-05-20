using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Make Dirt")]
public class MakeDirt : ToolsAction
{
    [SerializeField] List<TileBase> canMakeDirt;
    [SerializeField] AudioClip onPlowUsed;

    public override bool OnApplyToTileMap(Vector3Int gridPosition,
        TileMapReadController tileMapReadController,
        Item item)
    {
        //Debug.Log("OnApplyToTileMap");
        if (canMakeDirt == null)
        {
            Debug.Log("canPlow null");
            return true;
        }
        //check if that tile is center
        TileBase tileToMake;
        Vector3Int positionCheck = new Vector3Int();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                positionCheck.x = gridPosition.x + i;
                positionCheck.y = gridPosition.y + j;

                tileToMake = tileMapReadController.GetTileBase(positionCheck);
                if (!canMakeDirt.Contains(tileToMake))
                {
                    return false;
                }
            }
        bool checknew = true;

        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                positionCheck.x = gridPosition.x + i;
                positionCheck.y = gridPosition.y + j;

                if (tileMapReadController.baseManager.CanDirt(positionCheck))
                {
                    tileMapReadController.baseManager.MakeDirt(positionCheck);
                }
                else
                {
                    checknew = false;
                }
            }

        MusicManager.Instance.PlayEfx(onPlowUsed);

        return checknew;
    }
}
