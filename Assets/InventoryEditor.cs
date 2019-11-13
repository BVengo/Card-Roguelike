using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    [SerializeField]
    Item item;

    [SerializeField]
    int count;

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        Inventory inventory = target as Inventory;

        EditorGUILayout.Space();

        foreach (KeyValuePair<Item, int> pair in inventory)
        {
            EditorGUILayout.LabelField(pair.Key.DisplayName, pair.Value.ToString());
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        item = EditorGUILayout.ObjectField(item, typeof(Item), false) as Item;
        count = EditorGUILayout.IntField(count);
        EditorGUILayout.EndHorizontal();
        
        if (GUILayout.Button("Add"))
        {
            inventory.Add(item, count);
        }
    }
}
