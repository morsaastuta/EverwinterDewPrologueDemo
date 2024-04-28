using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalSkill : Skill
{
    protected List<Type> wields = new List<Type>();

    protected List<Type> AnyWield()
    {
        return new List<Type>
        {
            null,
            typeof(SwordItem),
            typeof(ShieldItem),
            typeof(BowItem)
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
}
