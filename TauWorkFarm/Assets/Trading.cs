using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject toolBarPanel;
    [SerializeField] GameObject QuantityPanelObject;
    [SerializeField] ItemContainer playerInventory;
    [SerializeField] ItemPanel inventoryItemPanel;

    [SerializeField] GameObject BuyButton;
    [SerializeField] GameObject SellButton;

    Store store;

    Currency money;

    ItemStorePanel itemStorePanel;

    QuantityPanel quantityPanel;

    int itemID = -1;
    //Item item;
    Slot itemSlot;

    private void Awake()
    {
        money = GetComponent<Currency>();
        itemStorePanel = storePanel.transform.GetChild(0).GetComponent<ItemStorePanel>();
        //itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
        quantityPanel = QuantityPanelObject.GetComponent<QuantityPanel>();
    }

    public void BeginTrading(Store _store)
    {
        store = _store;

        itemStorePanel.SetInventory(_store.storeContainer);
    }

    public void StopTrading()
    {
        store = null;
    }

    public bool CheckStore()
    {
        return store != null;
    }

    public void ShowStorePanel(bool show)
    {
        ButtonBuy(false).ButtonSell(false);
        storePanel.SetActive(show);
    }

    public void ShowInventoryPanel(bool show)
    {
        ButtonBuy(false).ButtonSell(false);
        inventoryPanel.SetActive(show);
        toolBarPanel.SetActive(!show);
    }

    public QuantityPanel ShowQuantityPanel(int id, Slot _itemSlot, bool isBuy)
    {
        itemID = id;
        //item = _itemSlot.item;
        itemSlot = _itemSlot;
        //Item item = store.storeContainer.slots[itemID].item;
        if (isBuy)
        {
            quantityPanel.SetSinglePrice = (int)(itemSlot.item.price * store.sellToPlayerMultip);
        }
        else
        {
            quantityPanel.max = itemSlot.count;
            quantityPanel.SetSinglePrice = (int)(itemSlot.item.price * store.buyFromPlayerMultip);
        }
        quantityPanel.SetIcon = itemSlot.item.icon;
        quantityPanel.UpdateUI();
        quantityPanel.ShowAll(true);
        if (!itemSlot.item.stackable)
        {
            quantityPanel.ButtonDecrease(false).ButtonIncrease(false);
        }
        return quantityPanel;
        //QuantityPanelObject.SetActive(true);

    }

    public Trading ButtonBuy(bool show)
    {
        BuyButton.SetActive(show);
        return this;
    }

    public Trading ButtonSell(bool show)
    {
        SellButton.SetActive(show);
        return this;
    }

    public void BuyItem()
    {
        if (itemID == -1) { return; }
        //Item itemToBuy = store.storeContainer.slots[itemID].item;
        int totalPrice = quantityPanel.GetTotalPrice;
        if (money.Check(totalPrice))
        {
            money.Decrease(totalPrice);
            playerInventory.Add(itemSlot.item, quantityPanel.count);
            //inventoryItemPanel.Show();
            //QuantityPanelObject.SetActive(false);
            quantityPanel.ShowAll(false);
        }
    }

    public void SellItem()
    {
        if (itemSlot.item == null) { return; }
        if (!itemSlot.item.canBeSold) { return; }
        int moneyGain = quantityPanel.GetTotalPrice;
        money.Add(moneyGain);
        int remain = itemSlot.count - quantityPanel.count;
        quantityPanel.ShowAll(false);
        inventoryItemPanel.UpdateCount();
        if (remain <= 0)
        {
            itemSlot.Clear();
            return;
        }
        itemSlot.Set(itemSlot.item, remain);


        //if (GamesManager.Instance.dragAndDropController.CheckForSale())
        //{
        //    Slot itemToSell = GamesManager.Instance.dragAndDropController.slot;

        //    int moneyGain = itemToSell.item.stackable == true ? 
        //        (int)(itemToSell.item.price * itemToSell.count * store.buyFromPlayerMultip): //total money gain if item stackable
        //        (int)(itemToSell.item.price * store.buyFromPlayerMultip); //total money gain if item not stackable
        //    money.Add(moneyGain);
        //    itemToSell.Clear();
        //    GamesManager.Instance.dragAndDropController.UpdateIcon();
        //}
    }
}
