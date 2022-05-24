using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeItemListPanel : MonoBehaviour
{
    public List<Slot_UI> slots = new List<Slot_UI>();

    private void Start()
    {
        
    }
    public void SetIndex()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetIndex(i);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetEmpty();
        }
    }

    public void Show(List<Slot> recipeElements)
    {
        for (int i = 0; i < recipeElements.Count; i++)
        {
            slots[i].SetItem(recipeElements[i]);
        }

        transform.parent.gameObject.SetActive(true);
    }
}
