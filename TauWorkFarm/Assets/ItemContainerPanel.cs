using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        //Debug.Log("Inventory Panel");
        GamesManager.Instance.dragAndDropController.OnClick(itemContainer.slots[id]);

        Show();
    }
}
