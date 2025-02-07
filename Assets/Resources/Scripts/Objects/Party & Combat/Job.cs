using Sirenix.Serialization;
using System;

[Serializable]
public abstract class Job
{
    [OdinSerialize] public string name;
    [OdinSerialize] public string description;
    [OdinSerialize] public int level;
    [OdinSerialize] public int experience;
    [OdinSerialize] public Type wield1;
    [OdinSerialize] public Type wield2;

    public virtual void CheckNewSkills(Profile character)
    {
    }

    public void LevelUp(Profile character)
    {
        level++;
        CheckNewSkills(character);
    }
}