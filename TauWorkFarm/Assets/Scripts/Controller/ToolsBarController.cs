using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 8;
    [SerializeField] ToolBarPanel toolBarPanel;
    [SerializeField] IconHighlight iconHighlight;
    int selectedTool;

    public Action<int> onChange;

    public void UpdateCount()
    {
        toolBarPanel.UpdateCount();
    }

    public Slot GetItemSlot
    {
        get
        {
            return GamesManager.Instance.inventoryContainer.slots[selectedTool];
        }
    }

    public Item GetItem
    {
        get
        {
            return GamesManager.Instance.inventoryContainer.slots[selectedTool].item;
        } 
    }

    private void Start()
    {
        onChange += UpdateHihlightIcon;
        UpdateHihlightIcon(selectedTool);
    }

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;

        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            else
            {
                selectedTool -= 1;
                selectedTool = (selectedTool < 0? toolbarSize - 1 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }

    public void UpdateHihlightIcon(int id = 0)
    {
        Item item = GetItem;

        if (item == null)
        {
            iconHighlight.Show = false;
            return;
        }

        iconHighlight.Show = item.iconHighlight; // setter has function setactive
        if (item.iconHighlight)
        {
            iconHighlight.Set(item.icon);
        }
    }

}
