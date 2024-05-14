using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MixedSkill : MagicalSkill
{
    [OdinSerialize] protected List<Type> wields = new();

    protected List<Type> AnyWield()
    {
        return new List<Type>
        {
            null,
            typeof(SwordItem),
            typeof(ShieldItem),
            typeof(ShieldItem)
        };
    }

    public Sprite GetWield(int index)
    {
        if (index < wields.Count)
        {
            if (wields[index] == null) return Resources.Load<Sprite>("Sprites/Empty");
            else if (wields[index].Equals(typeof(SwordItem))) return Resources.LoadAll<Sprite>("Sprites/HUD/Items/framesheet")[9];
            else if (wields[index].Equals(typeof(ShieldItem))) return Resources.LoadAll<Sprite>("Sprites/HUD/Items/framesheet")[10];
            else if (wields[index].Equals(typeof(BowItem))) return Resources.LoadAll<Sprite>("Sprites/HUD/Items/framesheet")[11];
        }
        return Resources.Load<Sprite>("Sprites/Empty");
    }

    public bool ContainsWield(WieldItem wieldItem)
    {
        foreach (Type type in wields)
        {
            if (wieldItem is null)
            {
                if (type is null) return true;
            }
            else if (type is not null)
            {
                if (wieldItem.GetType().BaseType.Equals(type)) return true;
            }
        }

        return false;
    }
}
