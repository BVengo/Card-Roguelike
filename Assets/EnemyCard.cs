using UnityEngine;

[System.Serializable]
public class GrowingValue
{
    [SerializeField]
    int baseValue = 0;

    [SerializeField]
    int growthRate = 1;

    [SerializeField]
    int growthStep = 1;

    public int GetValue(GameCore core)
    {
        return baseValue + (core.Round / growthRate) * growthStep;
    }
}

[CreateAssetMenu]
public class EnemyCard : CardDefinition
{
    public Sprite Sprite => sprite;

    public ToolType ToolType => toolType;
    
    [SerializeField]
    Sprite sprite;

    [SerializeField]
    ToolType toolType;

    [SerializeField]
    GrowingValue healthMinimum;

    [SerializeField]
    GrowingValue healthMaximum;

    [SerializeField]
    GrowingValue damageMinimum;

    [SerializeField]
    GrowingValue damageMaximum;

    [SerializeField]
    EnemyCardDisplay prefab;


    public override CardData CreateData(GameCore core)
    {
        EnemyCardData data = new EnemyCardData(
            Random.Range(healthMinimum.GetValue(core), healthMaximum.GetValue(core)), 
            damageMinimum.GetValue(core), 
            damageMaximum.GetValue(core));

        data.RandomizeDamage();

        return data;
    }
    
    public override CardDisplay CreateDisplay(CardInstance instance)
    {
        EnemyCardDisplay result = Instantiate(prefab);

        result.Set(this, instance.Data as EnemyCardData);

        return result;
    }

    public override void Apply(GameCore core, CardInstance instance)
    {
        EnemyCardData enemyData = (EnemyCardData)instance.Data;

        enemyData.TakeDamage(1);
    }

    public override void EndOfRound(GameCore core, CardInstance instance)
    {
        EnemyCardData data = instance.Data as EnemyCardData;

        if (data.Health > 0)
        {
            core.PlayerStats.TakeDamage(data.Damage);

            data.RandomizeDamage();
        }

        base.EndOfRound(core, instance);

        if (IsSticky && data.Health <= 0)
        {
            core.CardManager.ReplaceCard(instance);
        }
    }
}


public class EnemyCardData : CardData
{
    public int Health { get; private set; }
    public int Damage { get; private set; }

    int initialHealth;
    int minDamage;
    int maxDamage;

    public EnemyCardData(int health, int minDamage, int maxDamage)
    {
        Health = health;

        initialHealth = health;

        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }

    public void RandomizeDamage()
    {
        Damage = Random.Range(minDamage, maxDamage);

        OnChange();
    }

    public void SetModifier(ToolInstance modifier)
    {
        Modifier = modifier;

        OnChange();
    }

    public void TakeDamage(int baseDamage)
    {
        if (Modifier != null && Modifier.Tool is Weapon weapon)
        {
            baseDamage = baseDamage * weapon.Multiply + weapon.Add;
        }

        TakeDamageRaw(baseDamage);
    }

    public void TakeDamageRaw(int baseDamage)
    {
        Health -= baseDamage;

        OnChange();
    }

    public override void Reset()
    {
        Health = initialHealth;

        base.Reset();
    }
}