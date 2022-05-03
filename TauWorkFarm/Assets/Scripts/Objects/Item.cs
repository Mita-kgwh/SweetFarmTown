using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
    public ToolsAction onAction;
    public ToolsAction onTileMapAction;
    public ToolsAction onItemUsed;
    public Crop crop;
    public bool iconHighlight;
}
