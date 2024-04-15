using UnityEngine;

public class PeltRimebear : MaterialItem
{
    public PeltRimebear()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[3];
        name = "Rimebear Pelt";
        description = "Pelt obtained from a Rimebear.";
        price = 10;
        stackable = true;
    }
}
