using System;

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
    }

    public override bool Consume(CellController target, CellController user)
    {
        target.combatant.statMP += 25;
        return true;
    }

    public override void Consume(Combatant user)
    {
        user.statMP += 25;
    }
}
