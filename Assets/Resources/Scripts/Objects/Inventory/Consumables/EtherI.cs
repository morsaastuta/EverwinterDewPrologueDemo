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
        description = "Recovers 5 MP for one party member.";
        price = 25;
        stackable = true;
        range = 1;
    }

    public override bool Consume(CellController target, CellController user, PlayerBehaviour player)
    {
        target.combatant.ChangeMP(5);
        player.inventory.Remove(this);
        return true;
    }

    public override void Consume(PlayerBehaviour player)
    {
        player.currentProfile.ChangeMP(5);
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new EtherI());
    }
}
