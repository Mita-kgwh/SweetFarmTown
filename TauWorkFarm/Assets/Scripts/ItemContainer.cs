using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<Slot> slots;

    public void Add(Item item, int count = 1)
    {
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
}
