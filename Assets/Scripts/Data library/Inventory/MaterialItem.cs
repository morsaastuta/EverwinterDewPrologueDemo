using Sirenix.Serialization;
using System;

[Serializable]
public abstract class MaterialItem : Item
{
    [OdinSerialize] public int price;

    public MaterialItem SpecializedRegeneration(MaterialItem item)
    {
        BasicRegeneration(item);

        item.price = price;

        return item;
    }
}
