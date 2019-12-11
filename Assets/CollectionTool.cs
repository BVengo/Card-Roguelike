using System;
using UnityEngine;

[CreateAssetMenu]
public class CollectionTool : Tool
{
    public int Multiply => multiply;

    public int Add => add;

    [SerializeField]
    int multiply = 1;

    [SerializeField]
    int add = 1;
    
    public override void Equipped(GameCore core, ToolInstance instance)
    {
        base.Equipped(core, instance);

        new CollectionToolSubscriber(core, this, instance);
    }
}

public class CollectionToolSubscriber
{
    public CollectionTool Tool { get; }

    public ToolInstance ToolInstance { get; }

    public GameCore Core { get; }

    public CollectionToolSubscriber(GameCore core, CollectionTool tool, ToolInstance instance)
    {
        Core = core;
        Tool = tool;
        ToolInstance = instance;

        instance.Destroyed += Unsubscribe;

        Core.CardManager.CardAdded += ModifyResourceCard;
        Core.CardManager.CardSelected += CheckUsed;

        foreach (CardInstance card in core.CardManager.Cards)
        {
            ModifyResourceCard(card);
        }
    }

    void Unsubscribe(ToolInstance instance)
    {
        Core.CardManager.CardAdded -= ModifyResourceCard;
        Core.CardManager.CardSelected -= CheckUsed;
        
        foreach (CardInstance card in Core.CardManager.Cards)
        {
            UnmodifyResourceCard(card);
        }
    }

    void ModifyResourceCard(CardInstance instance, int index = -1)
    {
        if (instance.Definition is ResourceCard resourceCard &&
            resourceCard.ToolType == Tool.Type &&
            instance.Data is ResourceCardData data &&
            data.Modifier == null)
        {
            data.SetCount(ToolInstance, data.Count * Tool.Multiply + Tool.Add);
        }
    }

    void UnmodifyResourceCard(CardInstance instance)
    {
        if (instance.Data.Modifier == ToolInstance)
        {
            instance.Data.Reset();
        }
    }

    void CheckUsed(CardInstance instance)
    {
        if (instance.Data.Modifier == ToolInstance)
        {
            ToolInstance.Use(1);
        }
    }
}
