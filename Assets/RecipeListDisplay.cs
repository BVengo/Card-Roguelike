using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeListDisplay : MonoBehaviour
{
    public RecipeManager RecipeManager => recipeManager;

    [SerializeField]
    RecipeManager recipeManager;

    [SerializeField]
    RecipeDisplay template;

    List<RecipeDisplay> recipeDisplays = new List<RecipeDisplay>();

    RecipeDisplay selected;

    void Awake()
    {
        template.Set(this, null);

        recipeManager.Changed += UpdateDisplay;

        gameObject.SetActive(false);
    }

    void UpdateDisplay()
    {
        int index = 0;
        
        foreach (Tool tool in recipeManager.Afforded)
        {
            Add(index++, tool, true);
        }

        foreach (Tool tool in recipeManager.Discovered)
        {
            if (recipeManager.Afforded.Contains(tool))
            {
                continue;
            }

            Add(index++, tool, false);
        }

        for (; index < recipeDisplays.Count; index++)
        {
            recipeDisplays[index].Set(this, null);
        }
    }

    void Add(int index, Tool tool, bool isAfforded)
    {
        if (index == recipeDisplays.Count)
        {
            RecipeDisplay display = Instantiate(template, template.transform.parent);

            display.Selected += UpdateSelected;
            display.Purchased += RecipeClicked;

            recipeDisplays.Add(display);
        }

        recipeDisplays[index++].Set(this, tool);
    }

    void UpdateSelected(RecipeDisplay display)
    {
        recipeManager.MarkOld(display.Tool);

        selected?.Deselect();

        selected = display;
    }

    void RecipeClicked(Tool tool)
    {
        if (recipeManager.Craft(tool))
        {
            selected?.Deselect();
        }
    }
}
