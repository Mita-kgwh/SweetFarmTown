using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public int id;
    public bool stackable;
    public Sprite icon;
    public ToolsAction onAction;
    public ToolsAction onTileMapAction;
    public ToolsAction onItemUsed;
    public Crop crop;
    public bool iconHighlight; //highlight grid to place obj
    public GameObject itemPrefab;
    public bool isWeapon;
    public int damage = 10;
    public int price = 100;
    public bool canBeSold = true;
    public FoodKind foodKind;
    public int foodAmount;
}
