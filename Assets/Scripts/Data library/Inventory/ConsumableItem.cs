using System;

[Serializable]
public abstract class ConsumableItem : Item
{
    public int price;

    // Use in combat
    public bool friendly;
    public int range = 0;
    public bool directional = false;
    public bool squared = false;
    public bool multitarget = false;

    public virtual bool Consume(CellController target, CellController user)
    {
        return false;
    }

    public virtual void Consume(Combatant user)
    {
    }
}
