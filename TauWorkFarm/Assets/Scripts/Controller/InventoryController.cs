using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject toolBarPanel;

    bool visible;

    private void Start()
    {
        visible = false;
        inventoryPanel.SetActive(visible);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

    }
    public void ToggleInventory()
    {
        visible = !visible;
        inventoryPanel.SetActive(visible);
        toolBarPanel.SetActive(!visible);
    }
}
