using UnityEngine;
using System;

[Serializable]
public abstract class Item
{
    public string name = "";
    public string description = "";
    protected string sheetPath = "Sprites/Empty";
    protected int sheetIndex = -1;
    public bool stackable = true;

    public Sprite GetIcon()
    {
        if (sheetIndex >= 0) return Resources.LoadAll<Sprite>(sheetPath)[sheetIndex];
        else return Resources.Load<Sprite>(sheetPath);
    }
}
