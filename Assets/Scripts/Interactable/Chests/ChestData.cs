using Sirenix.Serialization;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ChestData
{
    [OdinSerialize] public Dictionary<Item, int> content = new();
    [OdinSerialize] public bool open;

    public void UpdateData(ChestData c)
    {
        content = c.content;
        open = c.open;
    }

    public void UpdateContent(Dictionary<Item, int> newContent)
    {
        content = newContent;
    }
}
