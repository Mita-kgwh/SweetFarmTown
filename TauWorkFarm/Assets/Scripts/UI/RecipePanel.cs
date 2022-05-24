using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipePanel : ItemPanel
{
    [SerializeField] RecipeList recipeList;
    [SerializeField] Crafting crafting;
    [SerializeField] RecipeItemListPanel recipeItemListPanel;

    int selectedID;

    public override void Show()
    {
        for (int i = 0; i < recipeList.recipes.Count && i < slots.Count; i++)
        {
            slots[i].SetItem(recipeList.recipes[i].output);
        }
    }

    public override void OnClick(int id)
    {
        if (id >= recipeList.recipes.Count) { return; }

        recipeItemListPanel.Show(recipeList.recipes[id].recipeElements);

        selectedID = id;
    }

    public void CraftNow()
    {
        crafting.Craft(recipeList.recipes[selectedID]);
    }

}
