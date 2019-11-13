using UnityEngine;

[CreateAssetMenu]
public class ResourceCard : CardDefinition
{
    public Item Item => item;

    [SerializeField]
    Item item;

    [SerializeField]
    int countMinimum = 1;

    [SerializeField]
    int countMaximum = 3;

    [SerializeField]
    ResourceCardDisplay prefab;

    public override object CreateData()
    {
        return new ResourceCardData(Random.Range(countMinimum, countMaximum));
    }

    public override CardDisplay CreateDisplay(object data)
    {
        ResourceCardDisplay result = Instantiate(prefab);

        result.Set(this, data as ResourceCardData);

        return result;
    }

    public override void Apply(CardOffers offers, object data)
    {
        ResourceCardData resourceData = (ResourceCardData)data;

        offers.Inventory.Add(item, resourceData.Count);
    }
}

public class ResourceCardData
{
    public int Count { get; }
    
    public ResourceCardData(int count)
    {
        Count = count;
    }
}