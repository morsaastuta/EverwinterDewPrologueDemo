using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsWinter : LegsItem
{
    public LegsWinter()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[2];
        name = "Winter Boots";
        description = "Boots crafted from rimebear leather. They are of common use in the southern regions of Heimonas.";
        price = 65;

        statDFN = 6;
        statDFL = 3;
        statSPD = 3;

        GenStatList();
    }
}
