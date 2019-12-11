using UnityEngine;

[CreateAssetMenu]
public class ResourceCard : CardDefinition
{
    public Item Item => item;

    public ToolType ToolType => toolType;

    [SerializeField]
    Item item;

    [SerializeField]
    ToolType toolType;

    [SerializeField]
    int countMinimum = 1;

    [SerializeField]
    int countMaximum = 3;

    [SerializeField]
    ResourceCardDisplay prefab;

    public override CardData CreateData(GameCore core)
    {
        return new ResourceCardData(Random.Range(countMinimum, countMaximum));
    }

    public override CardDisplay CreateDisplay(CardInstance instance)
    {
        ResourceCardDisplay result = Instantiate(prefab);

        result.Set(this, instance.Data as ResourceCardData);

        return result;
    }

    public override void Apply(GameCore core, CardInstance instance)
    {
        ResourceCardData resourceData = (ResourceCardData)instance.Data;

        core.Inventory.Add(item, resourceData.Count);

        resourceData.SetCount(resourceData.Modifier, -1);
    }

    public override void EndOfRound(GameCore core, CardInstance instance)
    {
        base.EndOfRound(core, instance);

        ResourceCardData data = instance.Data as ResourceCardData;

        if (IsSticky && data.Count <= 0)
        {
            core.CardManager.ReplaceCard(instance);
        }
    }
}

public class CardData
{
    public ToolInstance Modifier { get; protected set; }

    public event System.Action Changed; 

    public virtual void Reset()
    {
        Modifier = null;

        OnChange();
    }

    protected void OnChange()
    {
        Changed?.Invoke();
    }
}

public class ResourceCardData : CardData
{
    public int Count { get; private set; }

    int initialCount;

    public ResourceCardData(int count)
    {
        Count = count;

        initialCount = count;
    }

    public void SetCount(ToolInstance modifier, int count)
    {
        Modifier = modifier;
        Count = count;

        OnChange();
    }

    public override void Reset()
    {
        Count = initialCount;

        base.Reset();
    }
}