using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    [OdinSerialize] public List<Item> items = new();
    [OdinSerialize] public List<int> stock = new();

    public void Add(Item item, int qty)
    {
        items.Add(item);
        stock.Add(qty);
    }

    public void Clear()
    {
        items.Clear();
        stock.Clear();
    }

    public int GetQty(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals(item))
            {
                return stock[i];
            }
        }

        return 0;
    }

    public void Remove(Item item)
    {
        if (stock[items.IndexOf(item)]-- <= 0) Delete(item);
    }

    public void Delete(Item item)
    {
        stock.RemoveAt(items.IndexOf(item));
        items.Remove(item);
    }

    public int GetSize()
    {
        return items.Count;
    }
}
