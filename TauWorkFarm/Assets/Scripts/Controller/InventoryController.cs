using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolBarPanel;

    bool visible;

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
            statusPanel.SetActive(!statusPanel.activeInHierarchy);
        }

    }
    public void ToggleInventory()
    {
        visible = !visible;
        inventoryPanel.SetActive(visible);
        //statusPanel.SetActive(visible);
        toolBarPanel.SetActive(!visible);
    }

    public void Open()
    {
        inventoryPanel.SetActive(true);
        //statusPanel.SetActive(true);
        toolBarPanel.SetActive(false);
    }

    public void Close()
    {
        inventoryPanel.SetActive(false);
        //statusPanel.SetActive(false);
        toolBarPanel.SetActive(true);
    }

}
