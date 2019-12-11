using UnityEngine;

public class PlayerStats : GameManager
{
    public int Health { get; private set; }

    [SerializeField]
    int maxHealth = 100;

    public override void Initialize(GameCore core)
    {
        base.Initialize(core);

        Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        Debug.Log($"Took {damage} damage. Health is now {Health}");
    }
}
