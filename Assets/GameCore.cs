using System.Collections;
using UnityEngine;

public delegate void CardInstanceHandler(CardInstance instance);
public delegate void CardInstanceIndexHandler(CardInstance instance, int index);

public class GameCore : MonoBehaviour
{
    public CardManager CardManager => cardManager;

    public Equipment Equipment => equipment;

    public Inventory Inventory => inventory;

    public PlayerStats PlayerStats => playerStats;

    public RecipeManager RecipeManager => recipeManager;

    public int Round { get; private set; }
    
    [SerializeField]
    CardManager cardManager;

    [SerializeField]
    Equipment equipment;

    [SerializeField]
    Inventory inventory;

    [SerializeField]
    PlayerStats playerStats;

    [SerializeField]
    RecipeManager recipeManager;

    void Awake()
    {
        playerStats.Initialize(this);
        equipment.Initialize(this);
        cardManager.Initialize(this);
        inventory.Initialize(this);
        recipeManager.Initialize(this);
    }



    public void SelectCard(CardInstance instance)
    {
        Round++;

        Debug.Log("Entering round " + Round);
        
        cardManager.EndRound(instance);
    }
}
