using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer itemContainer;

    public List<Slot_UI> slots = new List<Slot_UI>();

    public void UpdateCount()
    {
        itemContainer.isChanging = true;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
    }

    private void OnEnable()
    {
        Show();
    }

    private void LateUpdate()
    {
        //if (itemContainer == null) { return; }
        if (itemContainer.isChanging)
        {
            Show();
            itemContainer.isChanging = false;
        }
    }

    public void SetIndex()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetIndex(i);
        }
    }
    public virtual void Show()
    {
        for (int i = 0; i < itemContainer.slots.Count && i < slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                slots[i].SetEmpty();
            }
            else
            {
                slots[i].SetItem(itemContainer.slots[i]);
            }
        }
    }


    public virtual void OnClick(int id)
    {

    }
}
