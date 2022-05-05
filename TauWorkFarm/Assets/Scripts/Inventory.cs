using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public Item item;
    //public CollectableType type;
    public int count;
    //public int maxAllowed;
    //public Sprite icon;

    public Slot()
    {
        //type = CollectableType.NONE;
        count = 0;
        //maxAllowed = 99;
    }

    public void Copy(Slot _slot)
    {
        item = _slot.item;
        count = _slot.count;
    }

    public void Set(Item _item, int _count)
    {
        item = _item;
        count = _count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }

    //public bool CanAddItem()
    //{
    //    if (count < maxAllowed)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    public void AddItem(Collectable _item)
    {
        //this.type = _item.type;
        item.icon = _item.item.icon;
        count++;
    }
}

[System.Serializable]
public class Inventory
{
    public List<Slot> slots = new List<Slot>();

}
