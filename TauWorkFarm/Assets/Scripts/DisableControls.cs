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
    [SerializeField] GameObject inventoryButton;
    [SerializeField] GameObject craftButton;
    [SerializeField] GameObject joystickButton;
    [SerializeField] GameObject interactButton;
    [SerializeField] GameObject runButton;
    [SerializeField] GameObject statusPanel;

    private static DisableControls instance;
    public static DisableControls Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DisableControls>();
            return instance;
        }
    }


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

    public DisableControls JoyStick(bool active)
    {
        Joystick joystick = joystickButton.GetComponent<Joystick>();
        joystick.enabled = active;
        joystickButton.SetActive(active);
        return this;
    }
    public DisableControls ButtonInventory(bool active)
    {
        inventoryButton.SetActive(active);
        return this;
    }
    public DisableControls ButtonCraft(bool active)
    {
        craftButton.SetActive(active);
        return this;
    }

    public DisableControls ButtonInteract(bool active)
    {
        interactButton.SetActive(active);
        return this;
    }
    public DisableControls ButtonRun(bool active)
    {
        runButton.SetActive(active);
        return this;
    }
}
