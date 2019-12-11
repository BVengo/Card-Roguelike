using UnityEngine;

[CreateAssetMenu]
public class Weapon : Tool
{
    public int Multiply => multiply;

    public int Add => add;

    public int MultiplyOthers => multiplyOthers;

    public int AddOthers => addOthers;

    [SerializeField]
    int multiply = 1;

    [SerializeField]
    int add = 1;

    [SerializeField]
    int multiplyOthers = 0;

    [SerializeField]
    int addOthers = 0;

    public override void Equipped(GameCore core, ToolInstance instance)
    {
        base.Equipped(core, instance);

        new WeaponSubscriber(core, this, instance);
    }
}

public class WeaponSubscriber
{
    //TODO: Hook into Core.Strength
    private const int Strength = 1;

    public Weapon Tool { get; }

    public ToolInstance ToolInstance { get; }

    public GameCore Core { get; }

    public WeaponSubscriber(GameCore core, Weapon tool, ToolInstance instance)
    {
        Core = core;
        Tool = tool;
        ToolInstance = instance;

        instance.Destroyed += Unsubscribe;

        Core.CardManager.CardAdded += ModifyEnemy;
        Core.CardManager.CardSelected += CheckUsed;

        foreach (CardInstance card in core.CardManager.Cards)
        {
            ModifyEnemy(card);
        }
    }

    void Unsubscribe(ToolInstance instance)
    {
        Core.CardManager.CardAdded -= ModifyEnemy;
        Core.CardManager.CardSelected -= CheckUsed;

        foreach (CardInstance card in Core.CardManager.Cards)
        {
            UnmodifyEnemy(card);
        }
    }

    void ModifyEnemy(CardInstance instance, int index = -1)
    {
        if (instance.Definition is EnemyCard enemyCard &&
            enemyCard.ToolType == Tool.Type &&
            instance.Data is EnemyCardData data &&
            data.Modifier == null)
        {
            data.SetModifier(ToolInstance);
        }
    }

    void UnmodifyEnemy(CardInstance instance)
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

            if (Tool.MultiplyOthers != 0 || Tool.AddOthers != 0)
            {
                foreach (CardInstance other in Core.CardManager.Cards)
                {
                    if (other != instance && other.Data is EnemyCardData enemy)
                    {
                        enemy.TakeDamageRaw(Strength * Tool.MultiplyOthers + Tool.AddOthers);
                    }
                }
            }
        }
    }
}

