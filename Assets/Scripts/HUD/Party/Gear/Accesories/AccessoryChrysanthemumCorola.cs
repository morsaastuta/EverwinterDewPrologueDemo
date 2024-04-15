using UnityEngine;

public class AccessoryChrysanthemumCorola : AccessoryItem
{
    public AccessoryChrysanthemumCorola()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[3];
        name = "Chrysanthemum Corola";
        description = "Hair tie decorated with chrysanthemum petals by Theron. Resembles a golden corola.";
        price = 0;

        statDFL = 5;
        statSPD = 5;
        statASA = 5;
        statARA = 15;

        GenStatList();
    }
}
