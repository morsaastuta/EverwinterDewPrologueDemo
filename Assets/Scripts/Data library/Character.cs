using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Character
{
    // General info
    [OdinSerialize] public string name;
    [OdinSerialize] public string description;

    // Visuals
    [OdinSerialize] public string iconPath;
    [OdinSerialize] public string profilePath;
    [OdinSerialize] public string facePath;
    [OdinSerialize] public string spritesheetOWPath;
    [OdinSerialize] public string animatorOWPath;

    public Character()
    {
    }

    public Sprite GetIcon(int i)
    {
        return Resources.LoadAll<Sprite>(iconPath)[i];
    }

    public Sprite GetProfile(int i)
    {
        return Resources.LoadAll<Sprite>(profilePath)[i];
    }

    public Sprite GetFace(int i)
    {
        return Resources.LoadAll<Sprite>(facePath)[i];
    }

    public Sprite GetSpritesheetOW(int i)
    {
        return Resources.LoadAll<Sprite>(spritesheetOWPath)[i];
    }

    public RuntimeAnimatorController GetAnimatorOW()
    {
        return Resources.Load<RuntimeAnimatorController>(animatorOWPath);
    }
}
