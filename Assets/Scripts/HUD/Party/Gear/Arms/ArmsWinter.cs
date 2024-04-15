using UnityEngine;

public class ArmsWinter : ArmsItem
{
    public ArmsWinter()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[1];
        name = "Winter Gloves";
        description = "Gloves crafted from rimebear leather. They are of common use in the southern regions of Heimonas.";
        price = 50;

        statDFN = 6;
        statDFL = 3;

        GenStatList();
    }
}
