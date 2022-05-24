using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    [SerializeField] Trading trading;
    bool inStore;

    private void Start()
    {
        //this.slots.ForEach(x =>
        //{
        //    x.inventory = this;
        //});
        Init();
    }

    private void OnEnable()
    {
        inStore = trading.CheckStore();
        Show();
    }

    public override void OnClick(int id)
    {
        //Debug.Log("Inventory Panel");
        if (inStore)
        {
            trading.ShowQuantityPanel(id, itemContainer.slots[id], false).ButtonSell(true).ButtonBuy(false);
        }
        else
        {
            GamesManager.Instance.dragAndDropController.OnClick(itemContainer.slots[id]);
        }

        Show();
    }
}
