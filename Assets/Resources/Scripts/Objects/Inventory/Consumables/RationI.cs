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
        description = "Recovers 15 HP for one party member.";
        price = 15;
        stackable = true;
        range = 1;
    }

    public override bool Consume(CellController target, CellController user, PlayerBehaviour player)
    {
        target.combatant.ChangeHP(15);
        player.inventory.Remove(this);
        return true;
    }

    public override void Consume(PlayerBehaviour player)
    {
        player.currentProfile.ChangeHP(15);
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new RationI());
    }
}
