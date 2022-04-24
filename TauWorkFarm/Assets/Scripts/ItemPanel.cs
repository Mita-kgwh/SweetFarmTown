using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer itemContainer;

    public List<Slot_UI> slots = new List<Slot_UI>();

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

    public void SetIndex()
    {
        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            slots[i].SetIndex(i);
        }
    }
    public void Show()
    {
        for (int i = 0; i < itemContainer.slots.Count; i++)
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
