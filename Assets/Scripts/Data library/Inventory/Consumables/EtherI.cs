using System;
using UnityEngine;

[Serializable]
public class EtherI : ConsumableItem
{
    public EtherI()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 1;
        name = "Small Ether";
        description = "Recovers 25 MP to one party member.";
        price = 25;
        stackable = true;
        range = 1;
    }

    public override bool Consume(CellController target, CellController user, PlayerProperties player)
    {
        target.combatant.ChangeMP(25);
        player.inventory.Remove(this);
        return true;
    }

    public override void Consume(PlayerProperties player)
    {
        player.currentProfile.ChangeMP(25);
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new EtherI());
    }
}
