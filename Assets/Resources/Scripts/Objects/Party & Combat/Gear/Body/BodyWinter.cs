using System;

[Serializable]
public class BodyWinter : BodyItem
{
    public BodyWinter()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 0;
        name = "Winter Coat";
        description = "Coat crafted from rimebear leather. It is of common use in the southern regions of Heimonas.";
        price = 80;

        statDFN = 7;
        statDFL = 2;
        statPRA = 4;

        GenStatList();
    }
}
