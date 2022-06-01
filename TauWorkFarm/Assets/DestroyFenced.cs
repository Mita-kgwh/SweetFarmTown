using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Destroy Fenced")]
public class DestroyFenced : ToolsAction
{
    [SerializeField] AudioClip onDestroyUsed;
    public override bool OnApplyToTileMap(Vector3Int gridPosition,
        TileMapReadController tileMapReadController,
        Item item
        )
    {
        if (tileMapReadController.groundManager.DestroyFenced(gridPosition))
        {
            MusicManager.Instance.PlayEfx(onDestroyUsed);
            return true;
        }
        return false;
    }
}
