using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Recipe List")]
public class RecipeList : ScriptableObject
{
    public List<CraftingRecipe> recipes;

    internal void Clear()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            recipes[i].Clear();
        }
    }
}
