using UnityEngine;

[CreateAssetMenu]
public class ToolType : ScriptableObject
{
    public Sprite Silhouette => silhouette;

    [SerializeField]
    Sprite silhouette;
}
