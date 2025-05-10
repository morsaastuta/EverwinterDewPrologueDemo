using System;

[Serializable]
public class RationII : ConsumableItem
{
    public RationII()
    {
        sheetPath = "Sprites/HUD/Items/iconsheet";
        sheetIndex = 6;
        name = "Medium Ration";
        description = "Recovers 30 HP for one party member.";
        price = 30;
        stackable = true;
        range = 1;
    }

    public override bool Consume(CellController target, CellController user, PlayerBehaviour player)
    {
        target.combatant.ChangeHP(30);
        player.inventory.Remove(this);
        return true;
    }

    public override void Consume(PlayerBehaviour player)
    {
        player.currentProfile.ChangeHP(30);
    }

    public override Item Regenerate()
    {
        return SpecializedRegeneration(new RationII());
    }
}
