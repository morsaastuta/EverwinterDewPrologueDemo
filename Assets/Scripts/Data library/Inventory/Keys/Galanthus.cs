using UnityEngine;
using System;

[Serializable]
public class Galanthus : KeyItem
{
    public Galanthus()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 2;
        name = "Galanthus";
        description = "Still waiting for the first dew drop after Everwinter.";
        stackable = false;
    }
}