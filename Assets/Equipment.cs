using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Equipment : GameManager
{
    [SerializeField]
    Tool testingTool;

    List<ToolInstance> tools = new List<ToolInstance>();

    public override void Initialize(GameCore core)
    {
        base.Initialize(core);

        if (testingTool != null)
        {
            EquipTool(testingTool);
        }
    }

    public void EquipTool(Tool tool)
    {
        ToolInstance existing = tools.FirstOrDefault(item => item.Tool.Type == tool.Type);

        if (existing != null)
        {
            existing.Destroy();
        }

        ToolInstance instance = new ToolInstance(tool);

        tools.Add(instance);

        tool.Equipped(Core, instance);

        instance.Destroyed += UnequipTool;
    }

    public void UnequipTool(ToolInstance instance)
    {
        tools.Remove(instance);

        instance.Destroy();
    }
}
