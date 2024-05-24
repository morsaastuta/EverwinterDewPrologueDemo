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
        range = 1;
    }

    public override bool Consume(CellController target, CellController user, PlayerProperties player)
    {
        target.combatant.ChangeHP(200);
        player.inventory.Remove(this);
        return true;
    }

    public override void Consume(PlayerProperties player)
    {
        player.currentProfile.ChangeHP(200);
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new RationII());
    }
}
