using UnityEngine;
using System;

[Serializable]
public class ShieldFloe : ShieldItem
{
    public ShieldFloe()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 7;
        name = "Floe Shield";
        description = "Durable and sturdy shield made by Nikolaos himself with extreme care.";
        price = 0;

        statATK = 6;
        statDFN = 10;
        statACC = 5;
        statPSA = 20;

        GenStatList();
    }
}
