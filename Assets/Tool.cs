using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCount
{
    public Item Item => item;

    public int Count => count;

    [SerializeField]
    Item item;

    [SerializeField]
    int count;
}

[System.Serializable]
public class Cost
{
    public int ItemCount => items.Length;

    public int RequirementCount => requirements.Length;

    public IEnumerable<ItemCount> Items => items;

    public IEnumerable<ToolType> Requirements => requirements;

    [SerializeField]
    ItemCount[] items;

    [SerializeField]
    ToolType[] requirements;

    public bool Check(Inventory inventory)
    {
        foreach (ItemCount itemCount in items)
        {
            if (inventory[itemCount.Item] < itemCount.Count)
            {
                return false;
            }
        }

        return true;
    }

    public bool ConsumeFrom(Inventory inventory)
    {
        if (!Check(inventory))
        {
            return false;
        }

        foreach (ItemCount itemCount in items)
        {
            inventory.Remove(itemCount.Item, itemCount.Count);
        }

        return true;
    }
}

public delegate void ToolInstanceHandler(ToolInstance instance);

public class ToolInstance
{
    public Tool Tool { get; }

    public int Durability { get; private set; }

    public bool IsDestroyed { get; private set; }

    public event ToolInstanceHandler Used;
    public event ToolInstanceHandler Destroyed;

    public ToolInstance(Tool tool)
    {
        Tool = tool;

        Durability = tool.Durability;
    }

    public void Use(int usages)
    {
        Durability -= usages;

        Used?.Invoke(this);

        if (Durability <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        if (!IsDestroyed)
        {
            IsDestroyed = true;

            Destroyed?.Invoke(this);
        }
    }
}

public abstract class Tool : ScriptableObject
{
    public Sprite Sprite => sprite;

    public ToolType Type => type;

    public Cost Cost => cost;

    public int Durability => durability;

    public string DisplayName => displayName;

    public string Description => description;

    [SerializeField]
    string displayName = "Unnamed";

    [SerializeField, Multiline]
    string description = "No description";

    [SerializeField]
    Sprite sprite;

    [SerializeField]
    ToolType type;

    [SerializeField]
    Cost cost;

    [SerializeField]
    int durability = -1;
    
    public virtual void Equipped(GameCore core, ToolInstance instance)
    {

    }
}