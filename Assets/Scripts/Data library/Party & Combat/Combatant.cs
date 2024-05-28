using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Combatant : Character
{
    // General info
    [OdinSerialize] public int level;
    [OdinSerialize] public bool KO;

    // Point meters
    [OdinSerialize] public int currentHP;
    [OdinSerialize] public int currentMP;
    [OdinSerialize] public int currentAP;
    [OdinSerialize] public int currentFAT;
    [OdinSerialize] public int initAP = 1;

    // Visuals
    [OdinSerialize] public string spritesheetCSPath;
    [OdinSerialize] public string animatorCSPath;

    // Skills
    [OdinSerialize] public List<Skill> skillset = new();

    // Base stats
    [OdinSerialize] protected int baseHP = 0;
    [OdinSerialize] protected int baseMP = 0;
    [OdinSerialize] protected int baseAP = 0;
    [OdinSerialize] protected int baseFAT = 1000;
    [OdinSerialize] protected int baseATK = 0;
    [OdinSerialize] protected int baseDFN = 0;
    [OdinSerialize] protected int baseMAG = 0;
    [OdinSerialize] protected int baseDFL = 0;
    [OdinSerialize] protected int baseSPI = 0;
    [OdinSerialize] protected int baseSPD = 0;
    [OdinSerialize] protected int baseMOV = 0;
    [OdinSerialize] protected float baseACC = 100;
    [OdinSerialize] protected float baseCR = 5;
    [OdinSerialize] protected float baseCD = 50;
    [OdinSerialize] protected float basePSA = 100;
    [OdinSerialize] protected float basePRA = 100;
    [OdinSerialize] protected float baseASA = 100;
    [OdinSerialize] protected float baseARA = 100;
    [OdinSerialize] protected float baseSSA = 100;
    [OdinSerialize] protected float baseSRA = 100;
    [OdinSerialize] protected float baseHSA = 100;
    [OdinSerialize] protected float baseHRA = 100;

    // Actual stats
    [OdinSerialize] public int statAP;
    [OdinSerialize] public int statFAT;
    [OdinSerialize] public int statHP;
    [OdinSerialize] public int statMP;
    [OdinSerialize] public int statATK;
    [OdinSerialize] public int statDFN;
    [OdinSerialize] public int statMAG;
    [OdinSerialize] public int statDFL;
    [OdinSerialize] public int statSPI;
    [OdinSerialize] public int statSPD;
    [OdinSerialize] public int statMOV;
    [OdinSerialize] public float statACC;
    [OdinSerialize] public float statCR;
    [OdinSerialize] public float statCD;
    [OdinSerialize] public float statPSA;
    [OdinSerialize] public float statPRA;
    [OdinSerialize] public float statASA;
    [OdinSerialize] public float statARA;
    [OdinSerialize] public float statSSA;
    [OdinSerialize] public float statSRA;
    [OdinSerialize] public float statHSA;
    [OdinSerialize] public float statHRA;

    // Stat increment rates
    [OdinSerialize] protected float incrHP = 0;
    [OdinSerialize] protected float incrMP = 0;
    [OdinSerialize] protected float incrATK = 0;
    [OdinSerialize] protected float incrDFN = 0;
    [OdinSerialize] protected float incrMAG = 0;
    [OdinSerialize] protected float incrDFL = 0;
    [OdinSerialize] protected float incrSPI = 0;
    [OdinSerialize] protected float incrSPD = 0;

    public Combatant()
    {
        LoadStats();
    }

    public virtual void LoadStats()
    {
        LoadBaseStats();
    }

    protected void LoadBaseStats()
    {
        statHP = baseHP + (int)(incrHP * level);
        statMP = baseMP + (int)(incrMP * level);
        statAP = baseAP;

        statATK = baseATK + (int)(incrATK * level);
        statDFN = baseDFN + (int)(incrDFN * level);
        statMAG = baseMAG + (int)(incrMAG * level);
        statDFL = baseDFL + (int)(incrDFL * level);
        statSPI = baseSPI + (int)(incrSPI * level);

        statACC = baseACC;
        statCR = baseCR;
        statCD = baseCD;

        statSPD = baseSPD + (int)(incrSPD * level);
        statMOV = baseMOV;
        statFAT = baseFAT;

        statPSA = basePSA;
        statPRA = basePRA;
        statASA = baseASA;
        statARA = baseARA;
        statSSA = baseSSA;
        statSRA = baseSRA;
        statHSA = baseHSA;
        statHRA = baseHRA;
    }

    public void FullRestore()
    {
        currentHP = statHP;
        currentMP = statMP;
    }

    public void ReloadAP()
    {
        currentAP = initAP;
    }

    public void MaxFatigue()
    {
        currentFAT = statFAT;
    }

    public void ChangeHP(int quantity)
    {
        if (!KO)
        {
            if (currentHP + quantity > statHP) currentHP = statHP;
            else if (currentHP + quantity < 0)
            {
                currentHP = 0;
                KnockOut();
            }
            else currentHP += quantity;
        }
    }

    public void ChangeMP(int quantity)
    {
        if (currentMP + quantity > statMP) currentMP = statMP;
        else if (currentMP + quantity < 0) currentMP = 0;
        else currentMP += quantity;
    }

    public void ChangeAP(int quantity)
    {
        if (currentAP + quantity > statAP) currentAP = statAP;
        else if (currentAP + quantity < 0) currentAP = 0;
        else currentAP += quantity;
    }

    public void ChangeFAT(int quantity)
    {
        if (currentFAT + quantity > statFAT) currentFAT = statFAT;
        else if (currentFAT + quantity < 0) currentFAT = 0;
        else currentFAT += quantity;
    }

    public void KnockOut()
    {
        KO = true;
    }

    public void Revive(int healing)
    {
        KO = false;
        ChangeHP(healing);
    }

    public Sprite GetSpritesheetCS(int i)
    {
        return Resources.LoadAll<Sprite>(spritesheetCSPath)[i];
    }

    public RuntimeAnimatorController GetAnimatorCS()
    {
        return Resources.Load<RuntimeAnimatorController>(animatorCSPath);
    }

    public void NewSkill(Skill newSkill)
    {
        bool isNew = true;

        foreach (Skill skill in skillset) if (skill.GetType().Equals(newSkill.GetType())) isNew = false;

        if (isNew) skillset.Add(newSkill);
    }
}
