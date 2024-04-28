using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Job
{
    public Sprite icon;
    public string name;
    public string description;
    public Type wield1;
    public Type wield2;

    public virtual List<Skill> SkillsByLevel(int level)
    {
        return new List<Skill>();
    }
}