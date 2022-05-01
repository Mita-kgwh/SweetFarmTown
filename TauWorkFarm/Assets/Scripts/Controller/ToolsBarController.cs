using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 8;
    [SerializeField] ToolBarPanel toolBarPanel;
    int selectedTool;

    public Action<int> onChange;

    public Item GetItem
    {
        get
        {
            return GamesManager.Instance.inventoryContainer.slots[selectedTool].item;
        } 
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
    public void UpdateQuantity()
    {
        toolBarPanel.Show();
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }


}
