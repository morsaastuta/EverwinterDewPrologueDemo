using UnityEngine;
using System;

[Serializable]
public class SwordAthanas : SwordItem
{
    public SwordAthanas()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 5;
        name = "Athanas Sword";
        description = "Sword forged with great skill just for the Athanas bloodline to wield.";
        price = 0;

        statATK = 10;
        statACC = -5;
        statPSA = 25;

        GenStatList();
    }
}
