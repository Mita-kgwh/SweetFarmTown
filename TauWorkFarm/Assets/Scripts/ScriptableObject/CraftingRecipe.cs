using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<Slot> recipeElements;
    public Slot output;

    internal void Clear()
    {
        recipeElements.Clear();
        output = null;
    }
}
