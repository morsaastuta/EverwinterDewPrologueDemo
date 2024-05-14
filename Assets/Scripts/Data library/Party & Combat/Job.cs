using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable]
public abstract class Job
{
    [OdinSerialize] public string name;
    [OdinSerialize] public string description;
    [OdinSerialize] public Type wield1;
    [OdinSerialize] public Type wield2;

    public virtual List<Skill> SkillsByLevel(int level)
    {
        return new List<Skill>();
    }
}