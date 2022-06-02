using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsPController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Rigidbody2D rgbd2d;
    [SerializeField] float offsetDis = 1f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance;
    [SerializeField] ToolsAction onTilePickUp;
    [SerializeField] IconHighlight iconHighlight;
    [SerializeField] AttackController attackController;
    [SerializeField] GrabController grabController;
    [SerializeField] ToolsBarController toolsBarController;
    [SerializeField] Animator animator;
    [SerializeField] int weaponEnergyCost = 5;
    [SerializeField] GameObject toolRange;
    [SerializeField] GameObject grabArea;

    Vector3Int selectedTilePosition;
    bool selectable;
    bool grabable;

    private void Awake()
    {
        toolRange.transform.localScale = new Vector3(maxDistance * 2, maxDistance * 2, 1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WeaponAction();
        }
        CanSelectCheck();
        if (!player.ismoving)
        {
            SelectTile();
            //markerManager.Show(true);
            //iconHighlight.CanSelect = true; // setter has function SetActive
            //CanSelectCheck();
            Marker();
            ShowRange();
        }
        else
        {
            markerManager.Show(false);
            iconHighlight.CanSelect = false; // setter has function SetActive
            toolRange.SetActive(false);
        }

        ShowGrabArea();
        //SelectTile();
        //CanSelectCheck();
        //Marker();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (Input.mousePosition.x > (Screen.width / 4) 
        //        && Input.mousePosition.x < (Screen.width * 3 / 4)
        //        && Input.mousePosition.y > (Screen.width / 4) 
        //        && Input.mousePosition.y < (Screen.width * 3 / 4))
        //    {
        //        UseButtonPressed();
        //    }
        //}
        if (selectable && Input.GetMouseButtonDown(0))
        {
            UseButtonPressed();
            GrabThing();
        }
    }

    public void UseButtonPressed()
    {
        if (UseToolWorld())
        {
            return;
        }
        UseToolGrid();
    }

    private void WeaponAction()
    {
        Item item = toolsBarController.GetItem;

        if (item == null) { return; }
        if (!item.isWeapon) { return; }
        Vector2 position = rgbd2d.position + player.lastDirection * offsetDis;

        bool isattack = attackController.Attack(item.damage, player.lastDirection);

        if (isattack)
        {
            EnergyCost(weaponEnergyCost);
        }

    }

    private void GrabThing()
    {
        Item item = toolsBarController.GetItem;

        if (item == null) { return; }
        if (!item.isGrabItem) { return; }

        Debug.Log("grab thing click;");
        Vector2 position = rgbd2d.position + player.lastDirection * offsetDis;
        grabController.Grab(position);
    }

    private void EnergyCost(int energyCost)
    {
        //Item item = toolsBarController.GetItem;
        PlayerManager.Instance.GetTired(energyCost);
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        //Vector2 position = new Vector2(transform.position.x, transform.position.y);
        //position += player.lastDirection;
        //selectedTilePosition = tileMapReadController.GetGridPosition(position, false);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
        iconHighlight.CanSelect = selectable; // setter has function SetActive
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
        iconHighlight.cellPosition = selectedTilePosition;    
    }

    private void ShowRange()
    {
        Item item = toolsBarController.GetItem;
        if (item == null) 
        { 
            toolRange.SetActive(false); 
            return; 
        }
        if (item.onAction == null && item.onTileMapAction == null)
        {
            toolRange.SetActive(false);
            return;
        }
        toolRange.SetActive(true);
    }

    private void ShowGrabArea()
    {
        Item item = toolsBarController.GetItem;
        if (item == null)
        {
            grabArea.SetActive(false);
            return;
        }
        if (!item.isGrabItem)
        {
            grabArea.SetActive(false);
            return;
        }
        grabArea.transform.position = rgbd2d.position + player.lastDirection * offsetDis;
        grabArea.SetActive(true);
    }
    private void ShowAttackArea()
    {
        Item item = toolsBarController.GetItem;
        if (item == null)
        {
            grabArea.SetActive(false);
            return;
        }
        if (!item.isWeapon)
        {
            grabArea.SetActive(false);
            return;
        }
        grabArea.transform.position = rgbd2d.position + player.lastDirection * offsetDis;
        grabArea.SetActive(true);
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
            EnergyCost(item.onAction.energyCost);

            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GamesManager.Instance.inventoryContainer);
            }
        }

        return complete;
    }

    private void UseToolGrid()
    {
        //if (selectable)
        //{
            
        //}

        Item item = toolsBarController.GetItem;
        if (item == null)
        {
            PickUpTile();
            return;
        }
        if (item.onTileMapAction == null) { return; }

        animator.SetTrigger("Act");

        bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition,
            tileMapReadController,
            item);

        if (complete)
        {
            EnergyCost(item.onTileMapAction.energyCost);

            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GamesManager.Instance.inventoryContainer);
            }
        }
    }

    private void PickUpTile()
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rgbd2d.position + player.lastDirection * offsetDis, 0.5f);
    }
}
