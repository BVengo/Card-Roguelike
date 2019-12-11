using UnityEngine;

[CreateAssetMenu]
public class ToolSet : ScriptableObject
{
    public Tool[] Tools => tools;

    [SerializeField]
    Tool[] tools;
}