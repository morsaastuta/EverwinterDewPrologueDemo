using UnityEngine;
using System;

[Serializable]
public class RationI : ConsumableItem
{
    public RationI()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 0;
        name = "Small Ration";
        description = "Heals 100 HP to one party member.";
        price = 15;
        stackable = true;
    }

    public override bool Consume(CellController target, CellController user)
    {
        target.combatant.ChangeHP(100);
        return true;
    }

    public override void Consume(Combatant user)
    {
        user.ChangeHP(100);
    }
}