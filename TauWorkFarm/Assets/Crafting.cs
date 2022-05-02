using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;

    public void Craft(CraftingRecipe recipe)
    {
        if (!inventory.CheckFreeSpace())
        {
            Debug.Log("Not enough space to fit output");
            return;
        }

        for (int i = 0; i < recipe.recipeElements.Count; i++)
        {
            if (inventory.CheckItem(recipe.recipeElements[i]) == false)
            {
                Debug.Log("You dont have enough item");
                return;
            }
        }

        for (int i = 0; i < recipe.recipeElements.Count; i++)
        {
            inventory.Remove(recipe.recipeElements[i].item, recipe.recipeElements[i].count);
        }

        inventory.Add(recipe.output.item, recipe.output.count);
    }
}
