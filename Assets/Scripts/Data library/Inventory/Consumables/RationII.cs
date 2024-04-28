using UnityEngine;
using System;

[Serializable]
public class RationII : ConsumableItem
{
    public RationII()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 6;
        name = "Medium Ration";
        description = "Heals 200 HP to one party member.";
        price = 30;
        stackable = true;
    }

    public override bool Consume(CellController target, CellController user)
    {
        target.combatant.ChangeHP(200);
        return true;
    }

    public override void Consume(Combatant user)
    {
        user.ChangeHP(200);
    }
}
