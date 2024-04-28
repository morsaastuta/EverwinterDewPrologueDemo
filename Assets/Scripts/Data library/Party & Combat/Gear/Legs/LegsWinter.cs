using UnityEngine;
using System;

[Serializable]
public class LegsWinter : LegsItem
{
    public LegsWinter()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 2;
        name = "Winter Boots";
        description = "Boots crafted from rimebear leather. They are of common use in the southern regions of Heimonas.";
        price = 65;

        statDFN = 6;
        statDFL = 3;
        statSPD = 3;

        GenStatList();
    }
}
