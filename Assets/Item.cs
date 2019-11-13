using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string DisplayName => displayName;

    public Sprite Sprite => sprite;

    [SerializeField]
    string displayName;

    [SerializeField]
    Sprite sprite;
}