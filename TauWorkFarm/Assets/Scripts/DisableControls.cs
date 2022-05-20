using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControls : MonoBehaviour
{
    PlayerController playerController;
    ToolsPController toolsPController;
    InventoryController inventoryController;
    ToolsBarController toolsBarController;
    ItemContainerInteractController interactController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        toolsPController = GetComponent<ToolsPController>();
        inventoryController = GetComponent<InventoryController>();
        toolsBarController = GetComponent<ToolsBarController>();
        interactController = GetComponent<ItemContainerInteractController>();

    }

    public DisableControls DisableToolsPController()
    {
        toolsPController.enabled = false;
        return this;
    }

    public DisableControls EnableToolsPController()
    {
        toolsPController.enabled = true;
        return this;
    }

    public void DisableControl()
    {
        playerController.enabled = false;
        toolsPController.enabled = false;
        inventoryController.enabled = false;
        toolsBarController.enabled = false;
        interactController.enabled = false;
    }

    public void EnableControl()
    {
        playerController.enabled = true;
        toolsPController.enabled = true;
        inventoryController.enabled = true;
        toolsBarController.enabled = true;
        interactController.enabled = true;
    }
}
