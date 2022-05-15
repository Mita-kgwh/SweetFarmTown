using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] ItemContainer playerInventory;
    [SerializeField] ItemPanel inventoryItemPanel;

    Store store;

    Currency money;

    ItemStorePanel itemStorePanel;

    private void Awake()
    {
        money = GetComponent<Currency>();
        itemStorePanel = storePanel.transform.GetChild(0).GetComponent<ItemStorePanel>();
        //itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
    }

    public void BeginTrading(Store _store)
    {
        store = _store;

        itemStorePanel.SetInventory(_store.storeContainer);

        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }

    public void StopTrading()
    {
        store = null;
        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void BuyItem(int id)
    {
        Item itemToBuy = store.storeContainer.slots[id].item;
        int totalPrice = (int)(itemToBuy.price * store.sellToPlayerMultip);
        if (money.Check(totalPrice))
        {
            money.Decrease(totalPrice);
            playerInventory.Add(itemToBuy, store.storeContainer.slots[id].count);
            inventoryItemPanel.Show()
;        }
    }

    public void SellItem()
    {
        if (GamesManager.Instance.dragAndDropController.CheckForSale())
        {
            Slot itemToSell = GamesManager.Instance.dragAndDropController.slot;

            int moneyGain = itemToSell.item.stackable == true ? 
                (int)(itemToSell.item.price * itemToSell.count * store.buyFromPlayerMultip): //total money gain if item stackable
                (int)(itemToSell.item.price * store.buyFromPlayerMultip); //total money gain if item not stackable
            money.Add(moneyGain);
            itemToSell.Clear();
            GamesManager.Instance.dragAndDropController.UpdateIcon();
        }
    }
}
