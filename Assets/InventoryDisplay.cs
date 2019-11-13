using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;

    [SerializeField]
    ItemDisplay template;
    
    List<ItemDisplay> itemDisplays = new List<ItemDisplay>();

    void Awake()
    {
        template.Clear();

        inventory.Changed += UpdateDisplay;
    }

    void UpdateDisplay()
    {
        int index = 0;

        foreach (KeyValuePair<Item, int> pair in inventory)
        {
            if (index == itemDisplays.Count)
            {
                ItemDisplay display = Instantiate(template, template.transform.parent);
                
                itemDisplays.Add(display);
            }

            itemDisplays[index++].Set(pair.Key, pair.Value);
        }

        for (;index < itemDisplays.Count; index++)
        {
            itemDisplays[index].Clear();
        }
    }
}
