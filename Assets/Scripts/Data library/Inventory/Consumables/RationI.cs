using System;
using UnityEngine;

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
        range = 1;
    }

    public override bool Consume(CellController target, CellController user, PlayerProperties player)
    {
        target.combatant.ChangeHP(100);
        player.inventory.Remove(this);
        return true;
    }

    public override void Consume(PlayerProperties player)
    {
        player.currentProfile.ChangeHP(100);
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new RationI());
    }
}
