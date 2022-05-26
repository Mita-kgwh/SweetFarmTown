using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<Slot> slots;
    public bool isChanging; //isDirty
    public int slotCount;
    internal void Init()
    {
        slots = new List<Slot>();
        for (int i = 0; i < 21; i++) //careful
        {
            slots.Add(new Slot());
        }
    }
    public void Add(Item item, int count = 1)
    {
        isChanging = true;

        if (item.stackable)
        {
            Slot itemSlot = slots.Find(x => x.item == item); // kiem xem có item do chua
            // co roi thi tang count
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            // chua co thi tim o nao null gan vao
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null) //tim dc
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }    
            }
        }
        else
        {
            // them item non-stackable vao item container
            Slot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }

        }
    }

    public void Remove(Item itemToRemove, int count = 1)
    {
        isChanging = true;

        if (itemToRemove.stackable)
        {
            Slot slot = slots.Find(x => x.item == itemToRemove);
            if (slot == null) { return; }

            slot.count -= count;
            if (slot.count <= 0 )
            {
                slot.Clear();
            }
        }
        else
        {
            while (count > 0)
            {
                count -= 1;
                Slot slot = slots.Find(x => x.item == itemToRemove);
                if (slot == null) { return; }

                slot.Clear();
            }
        }
    }

    internal bool CheckFreeSpace()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }

        return false;
    }
    internal bool CheckItem(Slot checkingItem)
    {
        Slot slot = slots.Find(x => x.item == checkingItem.item);
        if (slot == null) { return false; }
        if (checkingItem.item.stackable)
        {
            return slot.count >= checkingItem.count;
        }
        return true;
    }
}
