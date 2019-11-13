using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public Inventory Inventory => inventory;

    [SerializeField]
    Inventory inventory;

    void Awake()
    {
        Instance = this;
    }
}
