using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IEnumerable<KeyValuePair<Item, int>>
{
    public event Action Changed;

    Dictionary<Item, int> items = new Dictionary<Item, int>();

    public int this[Item item]
    {
        get
        {
            if (!items.ContainsKey(item))
            {
                return 0;
            }

            return items[item];
        }
    }


    public void Add(Item item, int count = 1)
    {
        if (!items.ContainsKey(item))
        {
            items.Add(item, count);
        }
        else
        {
            items[item] += count;
        }

        if (items[item] <= 0)
        {
            items.Remove(item);
        }

        Changed?.Invoke();
    }

    public IEnumerator<KeyValuePair<Item, int>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<Item, int>>)items).GetEnumerator();
    }

    public void Remove(Item item, int count = 1)
    {
        if (!items.ContainsKey(item))
        {
            return;
        }

        items[item] -= count;

        if (items[item] <= 0)
        {
            items.Remove(item);
        }

        Changed?.Invoke();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<Item, int>>)items).GetEnumerator();
    }
}
