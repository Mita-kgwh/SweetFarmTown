using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// responsble for interact between player and any itemcontainer in the scene
public class ItemContainerInteractController : MonoBehaviour
{
    ItemContainer targerItemContainer;
    [SerializeField] InventoryController inventoryController;
    [SerializeField] ItemContainerPanel itemContainerPanel;
    [SerializeField] PlayerController player;
    Transform openedChest;
    
    [SerializeField] float maxDistance = 2.0f;

    private void Update()
    {
        if (openedChest != null)
        {
            float distance = Vector2.Distance(openedChest.position, transform.position);
            if (distance > maxDistance)
            {
                openedChest.GetComponent<ChestInteractable>().Close(player);
            }
        }
    }

    public void Open(ItemContainer itemContainer, Transform _openedChest)
    {
        targerItemContainer = itemContainer;
        itemContainerPanel.itemContainer = targerItemContainer;
        inventoryController.Open();
        itemContainerPanel.gameObject.SetActive(true);
        openedChest = _openedChest;
    }

    public void Close()
    {
        inventoryController.Close();
        itemContainerPanel.gameObject.SetActive(false);
        openedChest = null;
    }
}
