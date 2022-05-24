using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorePanel : ItemPanel
{
    [SerializeField] Trading trading;
    //public List<StoreSlot_UI> storeSlots;
    public override void OnClick(int id)
    {
        //if (GamesManager.Instance.dragAndDropController.slot.item == null)
        //{
        //    ShowQuantityPanel(id);
        //}
        //else
        //{
        //    SellItem();
        //}
        ShowQuantityPanel(id);
        
        Show();
    }

    private void ShowQuantityPanel(int id)
    {
        //trading.BuyItem(id);   
        trading.ShowQuantityPanel(id, itemContainer.slots[id], true).ButtonBuy(true).ButtonSell(false);
    }

    private void SellItem()
    {
        trading.SellItem();
    }

}
