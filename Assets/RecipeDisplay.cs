using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void ToolHandler(Tool instance);
public delegate void RecipeDisplayHandler(RecipeDisplay display);

public class RecipeDisplay : MonoBehaviour
{
    public Tool Tool { get; private set; }

    public event RecipeDisplayHandler Selected;
    public event ToolHandler Purchased;

    [SerializeField]
    Text title;

    [SerializeField]
    RectTransform content;
    
    [SerializeField]
    Text description;

    [SerializeField]
    Text uses;

    [SerializeField]
    Image image;

    [SerializeField]
    string usesFormat = "{0} uses";

    [SerializeField]
    List<CostDisplay> costs;

    [SerializeField]
    GameObject newFlag;

    [SerializeField]
    UnityEvent selected;

    [SerializeField]
    UnityEvent deselected;

    public void Set(RecipeListDisplay listDisplay, Tool tool)
    {
        if (tool == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (Tool == tool)
        {
            return;
        }

        Tool = tool;

        gameObject.SetActive(true);

        title.text = tool.DisplayName;
        description.text = tool.Description;
        uses.text = string.Format(usesFormat, tool.Durability);
        
        image.sprite = tool.Sprite;


        int index = 0;

        foreach (ItemCount cost in tool.Cost.Items)
        {
            if (index == costs.Count)
            {
                costs.Add(Instantiate(costs[0], costs[0].transform.parent));
            }

            costs[index].Set(cost.Item.Sprite, cost.Count);
            costs[index].gameObject.SetActive(true);

            index++;
        }
        
        for (;index < costs.Count; index++)
        {
            costs[index].gameObject.SetActive(false);
        }

        newFlag.SetActive(listDisplay.RecipeManager.NewDiscoveries.Contains(Tool));
    }

    void Update()
    {
        RectTransform rect = transform as RectTransform;

        float scale = rect.rect.height / content.rect.height;

        content.localScale = Vector3.one * Mathf.Min(1f, scale);
    }

    public void Deselect()
    {
        deselected.Invoke();
    }

    public void OnClicked()
    {
        newFlag.SetActive(false);

        Selected?.Invoke(this);

        selected?.Invoke();
    }
    
    public void OnPurchaseClicked()
    {
        Purchased?.Invoke(Tool);
    }
}
