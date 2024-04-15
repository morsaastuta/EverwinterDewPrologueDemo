using UnityEngine;

public class AccessoryBellSouvenir : AccessoryItem
{
    public AccessoryBellSouvenir()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[4];
        name = "Bell Charm";
        description = "Local souvenir from Kampanario designed after the bell from its belfry. Made from iron, has motifs of snowdrops engraved and emits a faint chime when shaken.";
        price = 25;

        statDFL = 5;

        GenStatList();
    }
}
