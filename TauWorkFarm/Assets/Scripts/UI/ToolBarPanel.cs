using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarPanel : ItemPanel
{
    public GameObject toolsBarPanel;
    [SerializeField] ToolsBarController toolsBarController;

    public override void OnClick(int id)
    {
        toolsBarController.Set(id);
        HighLight(id);
        //Debug.Log("Tool Bar Panel here");
    }

    int currentSelectedTool;

    private void Start()
    {
        Init();
        toolsBarController.onChange += HighLight;
        HighLight(0);
    }

    public void HighLight(int id)
    {
        slots[currentSelectedTool].HighLight(false);
        currentSelectedTool = id;
        slots[currentSelectedTool].HighLight(true);
    }

}
