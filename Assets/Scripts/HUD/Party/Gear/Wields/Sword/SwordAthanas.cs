using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAthanas : SwordItem
{
    public SwordAthanas()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[5];
        name = "Athanas Sword";
        description = "Sword forged with great skill just for the Athanas bloodline to wield.";
        price = 0;

        statATK = 10;
        statACC = -5;
        statPSA = 25;
    }
}
