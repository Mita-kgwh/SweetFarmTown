using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    private void Start()
    {
        //this.slots.ForEach(x =>
        //{
        //    x.inventory = this;
        //});
        Init();
    }

    public override void OnClick(int id)
    {
        //Debug.Log("Inventory Panel");
        GamesManager.Instance.dragAndDropController.OnClick(itemContainer.slots[id]);
        
        Show();
    }
}
