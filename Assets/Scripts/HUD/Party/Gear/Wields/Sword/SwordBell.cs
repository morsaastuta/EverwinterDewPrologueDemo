using UnityEngine;

public class SwordBell : SwordItem
{
    public SwordBell()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[6];
        name = "Bell of Stephanos";
        description = "Iron sword forged in the northern town of Kampanario and designed after the bell from its belfry, as well as the bell-like local snowdrops. It emits a faint chime with each swing.";
        price = 20;

        statATK = 8;
        statACC = -10;
        statPRA = 10;

        GenStatList();
    }
}
