using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameCore Core { get; private set; }

    public virtual void Initialize(GameCore core)
    {
        Core = core;
    }
}
