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

    public virtual bool Consume(CellController target, CellController user, PlayerBehaviour player)
    {
        player.inventory.Remove(this);
        return false;
    }

    public virtual void Consume(PlayerBehaviour player)
    {
    }

    public void Drop(PlayerBehaviour player)
    {
        player.inventory.Remove(this);
    }

    public ConsumableItem SpecializedRegeneration(ConsumableItem item)
    {
        BasicRegeneration(item);

        item.price = price;
        item.friendly = friendly;
        item.range = range;
        item.directional = directional;
        item.squared = squared;
        item.multitarget = multitarget;

        return item;
    }
}
