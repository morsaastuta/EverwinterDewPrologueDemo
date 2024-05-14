using Sirenix.Serialization;
using System;

[Serializable]
public abstract class ConsumableItem : Item
{
    [OdinSerialize] public int price;

    // Use in combat
    [OdinSerialize] public bool friendly = true;
    [OdinSerialize] public int range = 0;
    [OdinSerialize] public bool directional = false;
    [OdinSerialize] public bool squared = false;
    [OdinSerialize] public bool multitarget = false;

    public virtual bool Consume(CellController target, CellController user)
    {
        return false;
    }

    public virtual void Consume(Combatant user)
    {
    }
}
