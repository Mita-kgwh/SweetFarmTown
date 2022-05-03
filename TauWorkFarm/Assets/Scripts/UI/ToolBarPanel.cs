using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarPanel : ItemPanel
{
    public GameObject toolsBarPanel;
    [SerializeField] ToolsBarController toolsBarController;
    int currentSelectedTool;

    private void Start()
    {
        Init();
        toolsBarController.onChange += HighLight;
        HighLight(0);
    }

    public override void OnClick(int id)
    {
        toolsBarController.Set(id);
        HighLight(id);
        //Debug.Log("Tool Bar Panel here");
    }

    public void HighLight(int id)
    {
        slots[currentSelectedTool].HighLight(false);
        currentSelectedTool = id;
        slots[currentSelectedTool].HighLight(true);
    }

    public override void Show()
    {
        base.Show();
        toolsBarController.UpdateHihlightIcon();
    }
}
