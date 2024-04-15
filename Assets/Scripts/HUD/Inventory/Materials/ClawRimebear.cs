using UnityEngine;

public class ClawRimebear : MaterialItem
{
    public ClawRimebear()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[5];
        name = "Rimebear Claw";
        description = "Claw obtained from a Rimebear.";
        price = 8;
        stackable = true;
    }
}
