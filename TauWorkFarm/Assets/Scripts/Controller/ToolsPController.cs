using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsPController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Rigidbody2D rgbd2d;
    [SerializeField] float offsetDis = 1f;
    [SerializeField] float sizeOfInteractableArea = 0.7f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    //[SerializeField] CropsManager cropsManager;
    //[SerializeField] TileData plowableTiles;
    [SerializeField] ToolsBarController toolsBarController;
    [SerializeField] Animator animator;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld()) 
            {
                return;
            }
            UseToolGrid();
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld() // interact with physical object
    {
        Vector2 position = rgbd2d.position + player.lastDirection * offsetDis;

        Item item = toolsBarController.GetItem;

        if (item == null) { return false; }
        if (item.onAction == null) { return false; }

        animator.SetTrigger("Act");
        bool complete = item.onAction.OnApply(position);

        if (complete)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GamesManager.Instance.toolsBarContainer);
            }
        }

        return complete;
    }

    private void UseToolGrid()
    {
        if (selectable)
        {
            Item item = toolsBarController.GetItem;
            if (item == null) { return; }
            if (item.onTileMapAction == null) { return; }

            animator.SetTrigger("Act");
         
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController);

            if (complete)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GamesManager.Instance.toolsBarContainer);
                }
            }
        }
    }

}