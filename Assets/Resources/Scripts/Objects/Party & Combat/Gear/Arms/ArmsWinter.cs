using System;

[Serializable]
public class ArmsWinter : ArmsItem
{
    public ArmsWinter()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 1;
        name = "Winter Gloves";
        description = "Gloves crafted from rimebear leather. They are of common use in the southern regions of Heimonas.";
        price = 50;

        statDFN = 4;
        statDFL = 1;
        statSPI = 1;
        statPRA = 2;

        GenStatList();
    }
}
