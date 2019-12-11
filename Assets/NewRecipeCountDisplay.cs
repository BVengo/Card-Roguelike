using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRecipeCountDisplay : MonoBehaviour
{
    [SerializeField]
    RecipeManager recipeManager;

    [SerializeField]
    string format = "{0} New!";

    [SerializeField]
    Text text;

    void Awake()
    {
        recipeManager.Changed += UpdateText;

        UpdateText();
    }

    void UpdateText()
    {
        if (recipeManager.DiscoveryCount > 0)
        {
            text.text = string.Format(format, recipeManager.DiscoveryCount);
        }
        else
        {
            text.text = "";
        }
    }
}
