using System;
using System.Collections.Generic;
using UnityEngine;

public class ToolNameSorter : IComparer<Tool>
{
    int IComparer<Tool>.Compare(Tool x, Tool y)
    {
        return x.name.CompareTo(y.name);
    }
}

public class RecipeManager : GameManager
{
    public event System.Action Changed;

    public int DiscoveryCount => newDiscoveries.Count;

    public IEnumerable<Tool> Discovered => discovered;
    public IEnumerable<Tool> Afforded => afforded;
    public IEnumerable<Tool> NewDiscoveries => newDiscoveries;

    [SerializeField]
    ToolSet toolSet;

    SortedSet<Tool> discovered = new SortedSet<Tool>(new ToolNameSorter());
    SortedSet<Tool> afforded = new SortedSet<Tool>(new ToolNameSorter());
    HashSet<Tool> newDiscoveries = new HashSet<Tool>();

    public override void Initialize(GameCore core)
    {
        base.Initialize(core);

        core.Inventory.Changed += InventoryChanged;
    }

    void InventoryChanged()
    {
        bool isChanged = false;

        foreach (Tool tool in toolSet.Tools)
        {
            if (CanMake(tool))
            {
                if (afforded.Add(tool))
                {
                    isChanged = true;
                }
                
                if (discovered.Add(tool))
                {
                    newDiscoveries.Add(tool);
                    Debug.Log("Discovered " + tool.DisplayName + "!");
                }
            }
            else
            {
                if (afforded.Remove(tool))
                {
                    isChanged = true;
                }
            }
        }

        if (isChanged)
        {
            Changed?.Invoke();
        }
    }

    public void MarkOld(Tool tool)
    {
        newDiscoveries.Remove(tool);

        Changed?.Invoke();
    }

    public bool Craft(Tool tool)
    {
        if (tool.Cost.ConsumeFrom(Core.Inventory))
        {
            Core.Equipment.EquipTool(tool);

            return true;
        }

        return false;
    }

    bool CanMake(Tool tool)
    {
        return tool.Cost.Check(Core.Inventory);
    }
}
