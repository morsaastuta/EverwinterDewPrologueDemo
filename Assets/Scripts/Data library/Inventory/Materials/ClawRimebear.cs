using System;

[Serializable]
public class ClawRimebear : MaterialItem
{
    public ClawRimebear()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 5;
        name = "Rimebear Claw";
        description = "Claw obtained from a Rimebear.";
        price = 8;
        stackable = true;
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new ClawRimebear());
    }
}
