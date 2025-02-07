using UnityEngine;
using System;

[Serializable]
public class PeltRimebear : MaterialItem
{
    public PeltRimebear()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 3;
        name = "Rimebear Pelt";
        description = "Pelt obtained from a Rimebear.";
        price = 10;
        stackable = true;
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new PeltRimebear());
    }
}
