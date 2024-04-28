using UnityEngine;
using System;

[Serializable]
public class HerbsThrascias : MaterialItem
{
    public HerbsThrascias()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 4;
        name = "Thrascias Herbs";
        description = "Herbs gathered from the cold forests of Thrascias.";
        price = 3;
        stackable = true;
    }
}
