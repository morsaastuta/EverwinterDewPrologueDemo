using UnityEngine;

public class FlowerSnowdrop : MaterialItem
{
    public FlowerSnowdrop()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[2];
        name = "Snowdrop";
        description = "Snowdrop gathered from the northern land of Heimonas.";
        price = 6;
        stackable = true;

    }
}
