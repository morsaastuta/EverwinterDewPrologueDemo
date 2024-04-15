using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyWinter : BodyItem
{
    public BodyWinter()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[0];
        name = "Winter Coat";
        description = "Coat crafted from rimebear leather. It is of common use in the southern regions of Heimonas.";
        price = 80;

        statDFN = 10;
        statDFL = 5;

        GenStatList();
    }
}
