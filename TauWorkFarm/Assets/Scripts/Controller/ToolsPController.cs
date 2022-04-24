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
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                return true;
            }
        }

        return false;
    }

    private void UseToolGrid()
    {
        TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
        TileData tileData = tileMapReadController.GetTileData(tileBase);
        
        if (tileData != plowableTiles) { return; }

        if (selectable)
        {
            if (cropsManager.CheckIsPlowed(selectedTilePosition))
            {
                cropsManager.Seed(selectedTilePosition);
            }
            else
            {
                cropsManager.Plow(selectedTilePosition);
            }
            
        }
    }

}
