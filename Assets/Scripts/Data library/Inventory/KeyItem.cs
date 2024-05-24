using System;
using Unity.VisualScripting;

[Serializable]
public abstract class KeyItem : Item
{
    public KeyItem SpecializedRegeneration(KeyItem item)
    {
        BasicRegeneration(item);

        return item;
    }
}
