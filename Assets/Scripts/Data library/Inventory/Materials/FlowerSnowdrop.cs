using System;

[Serializable]
public class FlowerSnowdrop : MaterialItem
{
    public FlowerSnowdrop()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 2;
        name = "Snowdrop";
        description = "Snowdrop gathered from the northern land of Heimonas.";
        price = 6;
        stackable = true;

    }
}
