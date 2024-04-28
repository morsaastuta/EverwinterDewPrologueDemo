using UnityEngine;
using System;

[Serializable]
public class BowAnemone : BowItem
{
    public BowAnemone()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 8;
        name = "Anemone Piercer";
        description = "Worn off bow decorated with anemones. A precious memento from Theron's past.";
        price = 0;

        statATK = 10;
        statSPD = 10;
        statACC = -10;
        statASA = 15;

        GenStatList();
    }
}
