using UnityEngine;

public class ShieldFloe : ShieldItem
{
    public ShieldFloe()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/gearsheet")[7];
        name = "Floe Shield";
        description = "Durable and sturdy shield made by Nikolaos himself with extreme care.";
        price = 0;

        statATK = 10;
        statDFN = 20;
        statACC = 20;
        statPSA = 25;

        GenStatList();
    }
}
