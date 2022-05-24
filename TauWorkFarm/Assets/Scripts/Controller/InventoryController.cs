using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolBarPanel;
    [SerializeField] GameObject additionalPanel;

    bool visible;
    DisableControls disableControls;
    private void Awake()
    {
        disableControls = DisableControls.Instance;
    }

    private void Start()
    {
        visible = false;
        inventoryPanel.SetActive(visible);
        statusPanel.SetActive(visible);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleCrafting();
        }

    }

    public void ToggleCrafting()
    {
        if (statusPanel.activeInHierarchy)
        {
            statusPanel.SetActive(false);
            disableControls.EnableToolsPController();
            
        }
        else
        {
            statusPanel.SetActive(true);
            disableControls.DisableToolsPController();
        }
    }

    public void ToggleInventory()
    {
        visible = inventoryPanel.activeInHierarchy;
        visible = !visible;
        inventoryPanel.SetActive(visible);
        //statusPanel.SetActive(visible);
        toolBarPanel.SetActive(!visible);
        if (visible)
        {
            disableControls.DisableToolsPController();
        }
        else
        {
            disableControls.EnableToolsPController();
        }
    }

    public void Open()
    {
        visible = true;
        inventoryPanel.SetActive(true);
        //statusPanel.SetActive(true);
        toolBarPanel.SetActive(false);
    }

    public void Close()
    {
        visible = false;
        inventoryPanel.SetActive(false);
        //statusPanel.SetActive(false);
        toolBarPanel.SetActive(true);
        additionalPanel.SetActive(false);
    }

}
