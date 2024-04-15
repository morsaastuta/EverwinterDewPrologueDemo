using UnityEngine;

public class RationII : ConsumableItem
{
    public RationII()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[6];
        name = "Medium Ration";
        description = "Heals 200 HP to one party member.";
        price = 30;
        stackable = true;
    }
}
