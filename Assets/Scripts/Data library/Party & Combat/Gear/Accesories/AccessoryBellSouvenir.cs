using UnityEngine;
using System;

[Serializable]
public class AccessoryBellSouvenir : AccessoryItem
{
    public AccessoryBellSouvenir()
    {
        sheetPath = "Sprites/HUD/Items/gearsheet";
        sheetIndex = 4;
        name = "Bell Charm";
        description = "Local souvenir from Kampanario designed after the bell from its belfry. Made from iron, has motifs of snowdrops engraved and emits a faint chime when shaken.";
        price = 25;

        statDFL = 5;

        GenStatList();
    }
}
