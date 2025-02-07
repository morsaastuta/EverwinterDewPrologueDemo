using System;

[Serializable]
public class AccessoryChrysanthemumCorola : AccessoryItem
{
    public AccessoryChrysanthemumCorola()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 3;
        name = "Chrysanthemum Corola";
        description = "Hair tie decorated with chrysanthemum petals by Theron. Resembles a golden corola.";
        price = 0;

        statDFL = 1;
        statSPD = 2;
        statASA = 5;
        statARA = 5;

        GenStatList();
    }
}
