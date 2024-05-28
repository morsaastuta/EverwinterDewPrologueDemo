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
        bool isNew = true;

        foreach(Item hadItem in items)
        {
            if (hadItem.GetType().Equals(item.GetType()))
            {
                AddStock(items.IndexOf(hadItem), qty);
                isNew = false;
            }
        }

        if (isNew)
        {
            items.Add(item);
            stock.Add(qty);
        }
    }

    public void AddStock(int index, int qty)
    {
        stock[index] += qty;
        if (stock[index] <= 0) Delete(items[index]);
    }

    public void Clear()
    {
        items.Clear();
        stock.Clear();
    }

    public int GetQty(Item item)
    {
        for (int i = 0; i < items.Count; i++) if (items[i].Equals(item)) return stock[i];

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
