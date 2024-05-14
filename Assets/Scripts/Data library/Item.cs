using UnityEngine;
using System;
using Unity.VisualScripting;
using Sirenix.Serialization;

[Serializable]
public abstract class Item
{
    [OdinSerialize] public string name = "";
    [OdinSerialize] public string description = "";
    [OdinSerialize] public string sheetPath = "Sprites/Empty";
    [OdinSerialize] protected int sheetIndex = -1;
    [OdinSerialize] public bool stackable = true;

    public Sprite GetIcon()
    {
        if (sheetIndex >= 0) return Resources.LoadAll<Sprite>(sheetPath)[sheetIndex];
        else return Resources.Load<Sprite>(sheetPath);
    }
}
